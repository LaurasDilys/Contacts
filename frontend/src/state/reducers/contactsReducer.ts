import * as actionTypes from '../actions/actionTypes';
import { ALL, ME } from '../../domain/contactTypes';
import { UserBasicInformation } from './otherUsersReducer';

type Contact = {
  id: string,
  type: string,
  receivedFrom: UserBasicInformation | null,
  sharedWith: UserBasicInformation[] | null,
  firstName: string | null,
  lastName: string | null,
  phoneNumber: string | null,
  alternativePhoneNumber: string | null,
  email: string | null,
  alternativeEmail: string | null,
  address: string | null,
  dateOfBirth: string | null,
  notes: string | null,
  selected: boolean
}

export type ContactsState = {
  selectedContacts: string,
  contacts: Contact[]
}

const initialState: ContactsState = {
  selectedContacts: ALL,
  contacts: []
}

type ContactsAction = {
  type: string,
  payload: Contact[] | Contact | string | null
}

const contactsReducer = (state: ContactsState = initialState, action: ContactsAction): ContactsState => {
  switch (action.type) {
    case actionTypes.GET_CONTACTS:
      return {
        ...state,
        contacts: action.payload as Contact[]
      }
    case actionTypes.NEW_CONTACT:
      const newContact = action.payload as Contact;
      newContact.selected = true;
      return {
        ...state,
        contacts: [...state.contacts, newContact]
      }
    case actionTypes.EDIT_CONTACT:
      const editedContact = action.payload as Contact;
      editedContact.selected = true;
      return {
        ...state,
        contacts: [...state.contacts.filter(c => c.id !== editedContact.id), editedContact]
      }
    case actionTypes.DELETE_CONTACT:
      return {
        ...state,
        contacts: [...state.contacts.filter(c => c.id !== action.payload as string)]
      }
    case actionTypes.SET_SELECTED_CONTACTS:
      return {
        ...state,
        selectedContacts: action.payload as string
      }
    case actionTypes.UPDATE_MY_CONTACT:
      let newState = state.contacts.filter(c => c.type !== ME)
      if (action.payload !== null) {
        let me = state.contacts.find(c => c.type === ME);
        me = { ...action.payload as Contact };
        newState.push(me);
      }
      return {
        ...state,
        contacts: [...newState]
      }
    default:
      return state;
  }
}

export default contactsReducer;
