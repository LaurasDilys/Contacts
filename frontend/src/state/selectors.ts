import { UserState } from './reducers/userReducer';
import { RootState } from './reducers/reducers';

export const userState = (state: RootState): UserState => state.user;
