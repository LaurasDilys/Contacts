import * as actionTypes from '../actions/actionTypes';

type User = {
  id: string,
  firstName: string,
  lastName: string,
  userName: string,
  showMyContact: Boolean,
  phoneNumber: string | null,
  alternativePhoneNumber: string | null,
  email: string | null,
  alternativeEmail: string | null,
  address: string | null,
  dateOfBirth: string | null,
  notes: string | null
}

export type UserState = {
  loggedIn: boolean,
  user: User
}

const initialState: UserState = {
  loggedIn: false,
  user: {
    id: '',
    firstName: '',
    lastName: '',
    userName: '',
    showMyContact: false,
    phoneNumber: '',
    alternativePhoneNumber: '',
    email: '',
    alternativeEmail: '',
    address: '',
    dateOfBirth: '',
    notes: ''
  },
};

type UserAction = {
  type: string,
  payload: User
};

const userReducer = (state: UserState = initialState, action: UserAction): UserState => {
  switch (action.type) {
    case actionTypes.LOGIN:
      return {
        ...state,
        loggedIn: true,
        user: { ...action.payload }
      };
    case actionTypes.LOGOUT:
      return initialState;
    case actionTypes.UPDATE_USER_INFORMATION:
      return {
        ...state,
        user: { ...action.payload }
      };
    default:
      return state;
  }
};

export default userReducer;
