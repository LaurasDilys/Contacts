import Api from '../../domain/Api';
import { deleteContactAction, editContactAction, newContactAction } from './contactsActions';

//
let i = 0;
//

export const newContact = (contact) => (dispatch) => {
  Api.post('test')
    .then(() => {
      //
      contact.id = ++i;
      //
      dispatch(newContactAction(contact));
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