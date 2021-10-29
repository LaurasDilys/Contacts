import * as actionTypes from '../actions/actionTypes';
import { ALL } from '../../domain/contactTypes';
import { UserBasicInformation } from './otherUsersReducer';

type Contact = {
  id: string,
  me: boolean,
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
    case actionTypes.UPDATE_CONTACT:
      const newContactInformation = action.payload as Contact;
      const newState = state.contacts.filter(c => c.id !== newContactInformation.id)
      let updatedContact = state.contacts.find(c => c.id === newContactInformation.id);
      updatedContact = { ...updatedContact, ...action.payload as Contact };
      newState.push(updatedContact);
      return {
        ...state,
        contacts: [...newState]
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
      const newStateAfterUpdateMyContact = state.contacts.filter(c => c.me)
      if (action.payload !== null) {
        let me = state.contacts.find(c => c.me);
        me = { ...me, ...action.payload as Contact };
        newStateAfterUpdateMyContact.push(me);
      }
      return {
        ...state,
        contacts: [...newStateAfterUpdateMyContact]
      }
    default:
      return state;
  }
}

export default contactsReducer;
