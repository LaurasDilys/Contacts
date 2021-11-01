import { combineReducers } from 'redux';
import type { StateType } from 'typesafe-actions';
import user from './userReducer';
import contacts from './contactsReducer';
import otherUsers from './otherUsersReducer'
import notification from './notificationReducer';

const reducers = combineReducers({ user, contacts, otherUsers, notification });
export type RootState = StateType<typeof reducers>;

export default reducers;
