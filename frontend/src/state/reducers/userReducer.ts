import * as actionTypes from '../actions/actionTypes';

export type UserState = {
  loggedIn: boolean;
  user: {
    id: string;
    userName: string;
    firstName: string;
    lastName: string;
  };
}

const initialState: UserState = {
  loggedIn: false,
  user: {
    id: '',
    userName: '',
    firstName: '',
    lastName: '',
  },
};

type UserAction = {
  type: string;
  payload: {
    id: string;
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
          id: action.payload.id,
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
