import * as actionTypes from './actionTypes';

export const getOtherUsersAction = props => ({
  type: actionTypes.GET_OTHER_USERS,
  payload: props
});