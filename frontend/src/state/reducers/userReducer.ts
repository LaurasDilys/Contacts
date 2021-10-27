import * as actionTypes from '../actions/actionTypes';

export type UserState = {
  loggedIn: boolean;
  user: {
    id: string;
    firstName: string;
    lastName: string;
    userName: string;
    showMyContact: Boolean
  };
}

const initialState: UserState = {
  loggedIn: false,
  user: {
    id: '',
    firstName: '',
    lastName: '',
    userName: '',
    showMyContact: false
  },
};

type UserAction = {
  type: string;
  payload: {
    id: string;
    firstName: string;
    lastName: string;
    userName: string;
    showMyContact: Boolean
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
          lastName: action.payload.lastName,
          showMyContact: action.payload.showMyContact
        }
      };
    case actionTypes.LOGOUT:
      return initialState;
    default:
      return state;
  }
};

export default userReducer;
