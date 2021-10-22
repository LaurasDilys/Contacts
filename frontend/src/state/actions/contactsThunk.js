import Api from '../../domain/Api';
import { deleteContactAction, editContactAction, newContactAction as createContactAction } from './contactsActions';

//
let i = 0;
//

export const createContact = (contact) => (dispatch) => {
  Api.post('test')
    .then(() => {
      //
      contact.id = ++i;
      //
      dispatch(createContactAction(contact));
    });
}

export const editContact = (contact) => (dispatch) => {
  Api.post('test')
    .then(() => {
      dispatch(editContactAction(contact));
    });
}

export const deleteContact = (id) => (dispatch) => {
  Api.post('test')
    .then(() => {
      dispatch(deleteContactAction(id));
    });
}