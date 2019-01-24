import { Injectable, OnDestroy } from '@angular/core';
import { Store } from '@ngrx/store';
import { AppState } from '../store/state/app.state';
import { username, loggedIn, accessToken } from '../store/selectors/user.selectors';
import { UpdateUser } from '../store/actions/user.actions';
import { Subscription } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

interface AccessTokenResponse {
  access_token: string;
}

interface DecodedAccessToken {
  username: string;
}

/*
 * Using ngrx to have ngrx
 */
@Injectable({
  providedIn: 'root'
})
export class AuthService implements OnDestroy {
  username: string;
  loggedIn: boolean;
  accessToken: string;
  lastError: any;

  private usernameSubscription: Subscription;
  private loggedInSubscription: Subscription;
  private accessTokenSubscription: Subscription;

  constructor(private store: Store<AppState>, private httpClient: HttpClient, private router: Router) {
    this.usernameSubscription = this.store.select(username).subscribe(e => this.username = e);
    this.loggedInSubscription = this.store.select(loggedIn).subscribe(e => this.loggedIn = e);
    this.accessTokenSubscription = this.store.select(accessToken).subscribe(e => this.accessToken = e);
  }

  ngOnDestroy(): void {
    this.usernameSubscription.unsubscribe();
    this.loggedInSubscription.unsubscribe();
    this.accessTokenSubscription.unsubscribe();
  }

  isAuthenticated() {
    const jwtHelperService = new JwtHelperService();
    return this.loggedIn && !jwtHelperService.isTokenExpired(this.accessToken);
  }

  logIn(email: string, password: string) {
    this.httpClient.post('/api/auth/login', {
      login: email,
      password: password
    })
    .subscribe(
      e => this.onLogIn(e as AccessTokenResponse),
      e => this.lastError = e,
    );
  }

  logOut() {
    this.store.dispatch(new UpdateUser({
      loggedIn: false,
    }));
  }

  onLogIn(userData: AccessTokenResponse) {
    const jwtHelperService = new JwtHelperService();
    const decodedAccessToken: DecodedAccessToken = jwtHelperService.decodeToken(userData.access_token);
    this.store.dispatch(new UpdateUser({
      username: decodedAccessToken.username,
      loggedIn: true,
      accessToken: userData.access_token,
    }));

    this.lastError = '';
    this.loggedIn = true;
    this.accessToken = userData.access_token;
    this.router.navigateByUrl('/');
  }
}
