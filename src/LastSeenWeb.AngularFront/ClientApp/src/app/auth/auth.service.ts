import { Injectable, OnDestroy } from '@angular/core';
import { Store } from '@ngrx/store';
import { AppState } from '../store/state/app.state';
import { username, loggedIn, accessToken } from '../store/selectors/user.selectors';
import { UpdateUser } from '../store/actions/user.actions';
import { Subscription } from 'rxjs';

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

  private usernameSubscription: Subscription;
  private loggedInSubscription: Subscription;
  private accessTokenSubscription: Subscription;

  constructor(private store: Store<AppState>) {
    this.usernameSubscription = this.store.select(username).subscribe(e => this.username = e);
    this.loggedInSubscription = this.store.select(loggedIn).subscribe(e => this.loggedIn = e);
    this.accessTokenSubscription = this.store.select(accessToken).subscribe(e => this.accessToken = e);
  }

  ngOnDestroy(): void {
    this.usernameSubscription.unsubscribe();
    this.loggedInSubscription.unsubscribe();
    this.accessTokenSubscription.unsubscribe();
  }

  logIn() {
  }

  logOut() {
    this.store.dispatch(new UpdateUser({
      loggedIn: false,
    }));
  }

  onLogIn() {
    this.store.dispatch(new UpdateUser({
      username: 'x',
      loggedIn: true,
      accessToken: 'y',
    }));
  }
}
