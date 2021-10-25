import * as actionTypes from '../actions/actionTypes';

type Contact = {
  id: string | null,
  firstName: string | null,
  lastName: string | null,
  phoneNumber: string | null,
  alternativePhoneNumber: string | null,
  email: string | null,
  alternativeEmail: string | null,
  dateOfBirth: string | null,
  notes: string | null,
  selected: boolean
}

export type ContactsState = {
  contacts: Contact[]
}

const initialState: ContactsState = {
  contacts: []
}

type ContactsAction = {
  type: string,
  payload: Contact[] | Contact | string
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
    default:
      return state;
  }
}

export default contactsReducer;
