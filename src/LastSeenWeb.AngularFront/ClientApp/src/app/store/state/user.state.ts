export interface UserState {
    username: string;
    accessToken: string;
    refreshToken: string;
}

export const initialUserState: UserState = {
  username: '',
  accessToken: '',
  refreshToken: '',
};
