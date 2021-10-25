import Api from '../../domain/Api';
import { deleteContactAction, editContactAction, getContactsAction, createContactAction } from './contactsActions';

export const getContacts = (id) => (dispatch) => {
  Api.get(`user/${id}/contacts`)
    .then(res => {
      dispatch(getContactsAction(res.data));
    })
}

export const createContact = (id, request) => (dispatch) => {
  Api.post(`user/${id}/contacts/create`, request)
    .then(res => {
      dispatch(createContactAction({...res.data}));
    });
}

export const editContact = (request) => (dispatch) => {
  Api.put(`contacts/${request.id}`, request)
    .then(res => {
      dispatch(editContactAction({...res.data}));
    });
}

export const deleteContact = (id) => (dispatch) => {
  Api.delete(`contacts/${id}`)
    .then(() => {
      dispatch(deleteContactAction(id));
    });
}