import { Injectable, OnDestroy } from '@angular/core';
import { Store } from '@ngrx/store';
import { AppState } from '../store/state/app.state';
import { user } from '../store/selectors/user.selectors';
import { UpdateUser } from '../store/actions/user.actions';
import { Subscription } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { TokenResponse } from '../shared/tokenresponse';
import { DecodedAccessToken } from '../shared/decodedaccesstoken';
import { LocalStorageAuthData } from './localstorageauthdata';

interface AuthData {
  username: string;
  accessToken: string;
  refreshToken: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService implements OnDestroy {
  private authData: AuthData;
  private userSubscription: Subscription;

  constructor(private store: Store<AppState>, private httpClient: HttpClient, private router: Router) {
    this.authData = new LocalStorageAuthData();
    this.userSubscription = this.store.select(user).subscribe(e => {
      this.authData.username = e.username;
      this.authData.accessToken = (e.accessToken != null && e.accessToken !== 'n/a') ? e.accessToken : this.authData.accessToken;
      this.authData.refreshToken = (e.refreshToken != null && e.refreshToken !== 'n/a') ? e.refreshToken : this.authData.refreshToken;
    });
    this.handleExistingLogin();
  }

  ngOnDestroy(): void {
    this.userSubscription.unsubscribe();
  }

  getToken() {
    return this.authData.accessToken;
  }

  async isAuthenticated(): Promise<boolean> {
    if (this.checkTokens()) {
      return true;
    }
    await this.refreshToken();
    return this.checkTokens();
  }

  private checkTokens(): boolean {
    const jwtHelperService = new JwtHelperService();
    if (!jwtHelperService.isTokenExpired(this.authData.accessToken)) {
      return true;
    }
    return false;
  }

  private async refreshToken(): Promise<void> {
    if (!this.authData.refreshToken) {
      return;
    }
    await this.httpClient.post<TokenResponse>('/api/auth/refresh', {
      refreshToken: this.authData.refreshToken,
    }).toPromise().then(e => {
      this.store.dispatch(new UpdateUser({
        accessToken: e && e.access_token || '',
        refreshToken: e && e.refresh_token || '',
      }));
    }).catch(e => { });
  }

  private handleExistingLogin() {
    if (this.checkTokens()) {
      const jwtHelperService = new JwtHelperService();
      const decodedAccessToken: DecodedAccessToken = jwtHelperService.decodeToken(this.authData.accessToken);
      this.store.dispatch(new UpdateUser({
        username: decodedAccessToken && decodedAccessToken.username || '',
        accessToken: this.authData.accessToken,
        refreshToken: this.authData.refreshToken,
      }));
    }
  }

  // private synchronousSleepHack(milisecondTimeout: number) {
  //   const start = new Date().getTime(), expire = start + milisecondTimeout;
  //   while (new Date().getTime() < expire) { }
  //   return;
  // }
}
