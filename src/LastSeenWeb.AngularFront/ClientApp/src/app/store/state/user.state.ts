export interface UserState {
    username: string;
    loggedIn: boolean;
    accessToken: string;
}

export const initialUserState: UserState = {
  username: '',
  loggedIn: false,
  accessToken: '',
};
