import { createSelector } from '@ngrx/store';
import { AppState } from '../state/app.state';
import { UserState } from '../state/user.state';

const userSlice = (state: AppState) => state.user;

export const username = createSelector(
    userSlice,
    (state: UserState) => state.username
);

export const loggedIn = createSelector(
    userSlice,
    (state: UserState) => state.loggedIn
);

export const accessToken = createSelector(
    userSlice,
    (state: UserState) => state.accessToken
);
