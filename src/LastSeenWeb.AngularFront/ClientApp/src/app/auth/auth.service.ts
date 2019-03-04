import { Injectable, OnDestroy } from '@angular/core';
import { Store } from '@ngrx/store';
import { AppState } from '../store/state/app.state';
import { user } from '../store/selectors/user.selectors';
import { UpdateUser } from '../store/actions/user.actions';
import { Subscription, Observable, of } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { catchError, tap } from 'rxjs/operators';

interface DecodedAccessToken {
  username: string;
}

export interface TokenResponse {
  access_token?: string;
  refresh_token?: string;
  error?: string;
}

export interface CheckEmailResponse {
  success?: string;
  error?: string;
}

export interface VoidResponse {
  error?: string;
}

class AuthData {
  username: string;
  get accessToken(): string {
    return localStorage.getItem('accessToken') || '';
  }
  set accessToken(value: string) {
    localStorage.setItem('accessToken', value);
  }
  get refreshToken(): string {
    return localStorage.getItem('refreshToken') || '';
  }
  set refreshToken(value: string) {
    localStorage.setItem('refreshToken', value);
  }
}

/*
 * Using ngrx to have ngrx
 */
@Injectable({
  providedIn: 'root'
})
export class AuthService implements OnDestroy {
  private authData: AuthData;
  private userSubscription: Subscription;

  constructor(private store: Store<AppState>, private httpClient: HttpClient, private router: Router) {
    this.authData = new AuthData();
    this.userSubscription = this.store.select(user).subscribe(e => {
      this.authData.username = e.username;
      this.authData.accessToken = !e.accessToken ? this.authData.accessToken : e.accessToken;
      this.authData.refreshToken = !e.refreshToken ? this.authData.refreshToken : e.refreshToken;
    });
    this.handleExistingLogin();
  }

  ngOnDestroy(): void {
    this.userSubscription.unsubscribe();
  }

  getToken(): string {
    return this.authData.accessToken;
  }

  async isAuthenticated(): Promise<boolean> {
    if (this.checkAuthenticated()) {
      return true;
    }
    await this.refreshToken();
    return this.checkAuthenticated();
  }

  register(email: string, password: string, password2: string): Observable<VoidResponse> {
    return this.httpClient.post<VoidResponse>('/api/auth/register', {
      email: email,
      password: password,
      password2: password2
    }).pipe(catchError(e => of({ error: (e as HttpErrorResponse).error.error })));
  }

  logIn(email: string, password: string): Observable<TokenResponse> {
    return this.httpClient.post<TokenResponse>('/api/auth/login', {
      login: email,
      password: password
    }).pipe(
      tap(e => this.onNewToken(e)),
      catchError(e => of({ error: e }))
    );
  }

  logOut() {
    this.store.dispatch(new UpdateUser({
      accessToken: '',
      refreshToken: '',
    }));
    localStorage.removeItem('accessToken');
    localStorage.removeItem('refreshToken');
    this.router.navigateByUrl('/');
  }

  checkEmail(userId: string, code: string): Observable<CheckEmailResponse> {
    return this.httpClient.get<CheckEmailResponse>('/api/auth/checkemail', {
      params: {
        'userId': userId || '',
        'code': code || '',
      }
    }).pipe(catchError(e => of({ error: 'Unable to process request' })));
  }

  forgotPassword(email: string): Observable<VoidResponse> {
    return this.httpClient.post<VoidResponse>('/api/auth/forgotpassword', {
      email: email
    }).pipe(catchError(e => of({ error: 'Unable to process request' })));
  }

  onNewToken(userData: TokenResponse) {
    const jwtHelperService = new JwtHelperService();
    const decodedAccessToken: DecodedAccessToken = jwtHelperService.decodeToken(userData.access_token);
    this.store.dispatch(new UpdateUser({
      username: decodedAccessToken && decodedAccessToken.username || '',
      accessToken: userData && userData.access_token || '',
      refreshToken: userData && userData.refresh_token || '',
    }));
  }

  private checkAuthenticated(): boolean {
    const jwtHelperService = new JwtHelperService();
    if (!jwtHelperService.isTokenExpired(this.authData.accessToken)) {
      return true;
    }
    return false;
  }

  private handleExistingLogin() {
    if (this.checkAuthenticated()) {
      const jwtHelperService = new JwtHelperService();
      const decodedToken: DecodedAccessToken = jwtHelperService.decodeToken(this.authData.accessToken);
      this.authData.username = decodedToken && decodedToken.username || '';
    }
  }

  private async refreshToken(): Promise<void> {
    if (!this.authData.refreshToken) {
      return;
    }
    await this.httpClient.post<TokenResponse>('/api/auth/refresh', {
      refreshToken: this.authData.refreshToken,
    }).toPromise().then(e => {
      const tokenResponse = e;
      this.store.dispatch(new UpdateUser({
        accessToken: tokenResponse && tokenResponse.access_token || '',
        refreshToken: tokenResponse && tokenResponse.refresh_token || '',
      }));
    }).catch(e => { });
  }

  // private synchronousSleepHack(milisecondTimeout: number) {
  //   const start = new Date().getTime(), expire = start + milisecondTimeout;
  //   while (new Date().getTime() < expire) { }
  //   return;
  // }
}
