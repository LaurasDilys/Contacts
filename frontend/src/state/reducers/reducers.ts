import { combineReducers } from 'redux';
import type { StateType } from 'typesafe-actions';
import user from './userReducer';

const reducers = combineReducers({ user });
export type RootState = StateType<typeof reducers>;

export default reducers;
