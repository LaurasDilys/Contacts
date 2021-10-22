import * as actionTypes from './actionTypes';

export const loginAction = props => ({
  type: actionTypes.LOGIN,
  payload: props,
});

export const logoutAction = () => ({
  type: actionTypes.LOGOUT,
});
