import * as actionTypes from './actionTypes';

export const setNotification = props => ({
  type: actionTypes.SET_NOTIFICATION,
  payload: props,
});

export const clearNotification = () => ({
  type: actionTypes.CLEAR_NOTIFICATION,
});

export const actionFrom = (type, responseOrError) => {
  let message;
  if (type === 'success') {
    message = responseOrError.data;
  } else if (type === 'error') {
    message = responseOrError.response ?
      responseOrError.response.data :
      responseOrError.message;
  }
  return {
    isOpen: true,
    message: message,
    type: type
  }
}