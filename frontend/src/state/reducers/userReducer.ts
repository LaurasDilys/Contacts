import * as actionTypes from '../actions/actionTypes';

export type UserState = {
  loggedIn: boolean;
  user: {
    userName: string;
    firstName: string;
    lastName: string;
  };
}

const initialState: UserState = {
  loggedIn: false,
  user: {
    userName: '',
    firstName: '',
    lastName: '',
  },
};

type UserAction = {
  type: string;
  payload: {
    userName: string;
    firstName: string;
    lastName: string;
  };
};

const userReducer = (state: UserState = initialState, action: UserAction): UserState => {
  switch (action.type) {
    case actionTypes.LOGIN:
      return {
        ...state,
        loggedIn: true,
        user: {
          userName: action.payload.userName,
          firstName: action.payload.firstName,
          lastName: action.payload.lastName
        }
      };
    case actionTypes.LOGOUT:
      return initialState;
    default:
      return state;
  }
};

export default userReducer;
