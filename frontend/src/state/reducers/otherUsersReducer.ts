import * as actionTypes from '../actions/actionTypes';

export type UserBasicInformation = {
  id: string,
  firstName: string,
  lastName: string,
  userName: string
}

export type OtherUsersState = {
  otherUsers: UserBasicInformation[]
}

const initialState: OtherUsersState = {
  otherUsers: []
}

type OtherUsersAction = {
  type: string,
  payload: UserBasicInformation[]
}

const otherUsersReducer = (state: OtherUsersState = initialState, action: OtherUsersAction): OtherUsersState => {
  switch (action.type) {
    case actionTypes.GET_OTHER_USERS:
      return {
        ...state,
        otherUsers: action.payload
      }
    case actionTypes.LOGOUT:
      return initialState;
    default:
      return state;
  }
}

export default otherUsersReducer;
