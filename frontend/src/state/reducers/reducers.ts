import { combineReducers } from 'redux';
import type { StateType } from 'typesafe-actions';
import user from './userReducer';
import contacts from './contactsReducer';
import otherUsers from './otherUsersReducer'

const reducers = combineReducers({ user, contacts, otherUsers });
export type RootState = StateType<typeof reducers>;

export default reducers;
