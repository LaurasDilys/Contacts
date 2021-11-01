import * as actionTypes from '../actions/actionTypes';

export type NotificationState = {
  isOpen: boolean;
  message: string;
  type: 'warning' | 'success' | 'error' | 'info';
}

const initialState: NotificationState = {
  isOpen: false,
  message: '',
  type: 'warning',
}

type NotificationAction = {
  type: string;
  payload: NotificationState;
}

const notificationReducer = (state: NotificationState = initialState, action: NotificationAction): NotificationState => {
  switch (action.type) {
    case actionTypes.SET_NOTIFICATION: {
      return { ...state, isOpen: true, message: action.payload.message, type: action.payload.type };
    }
    case actionTypes.CLEAR_NOTIFICATION: {
      return { ...state, isOpen: false };
    }
    case actionTypes.LOGOUT:
      return initialState;
    default:
      return state;
  }
}

export default notificationReducer;
