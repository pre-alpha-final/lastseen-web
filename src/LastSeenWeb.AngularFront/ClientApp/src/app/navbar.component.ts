import { Component, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { Store } from '@ngrx/store';
import { AppState } from './store/state/app.state';
import { accessToken } from './store/selectors/user.selectors';
import { AuthService } from './auth/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavBarComponent implements OnDestroy {
  private userSubscription: Subscription;
  private loggedIn: boolean;

  constructor(private store: Store<AppState>, private authService: AuthService) {
    this.userSubscription = this.store.select(accessToken).subscribe(e => this.loggedIn = !!e);
  }

  ngOnDestroy(): void {
    this.userSubscription.unsubscribe();
  }

  logOut(): void {
    this.authService.logOut();
  }
}
