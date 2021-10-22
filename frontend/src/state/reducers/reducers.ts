import { combineReducers } from 'redux';
import type { StateType } from 'typesafe-actions';
import contacts from './contactsReducer';
import user from './userReducer';

const reducers = combineReducers({ contacts, user });
export type RootState = StateType<typeof reducers>;

export default reducers;
