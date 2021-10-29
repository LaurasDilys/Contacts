import Api from '../../domain/Api';
import { deleteContactAction, updateContactAction, getContactsAction, createContactAction } from './contactsActions';

export const getContacts = (id) => (dispatch) => {
  Api.get(`users/${id}/contacts`)
    .then(res => {
      dispatch(getContactsAction(res.data));
    });
}

export const createContact = (id, request) => (dispatch) => {
  Api.post(`users/${id}/contacts`, request)
    .then(res => {
      dispatch(createContactAction({ ...res.data }));
    });
}

export const updateContact = (request) => (dispatch) => {
  Api.put(`contacts/${request.id}`, request)
    .then(res => {
      dispatch(updateContactAction({ ...res.data }));
    });
}

export const deleteContact = (id) => (dispatch) => {
  Api.delete(`contacts/${id}`)
    .then(() => {
      dispatch(deleteContactAction(id));
    });
}

export const shareContact = (contactId, userId) => (dispatch) => {
  Api.post(`contacts/${contactId}/sharewith/${userId}`)
    .then(res => {
      dispatch(updateContactAction({ ...res.data }));
    });
}

export const acceptSharedContact = (contactId, userId) => (dispatch) => {
  Api.post(`users/${userId}/acceptshare/${contactId}`)
    .then(res => {
      dispatch(updateContactAction({ ...res.data }));
    });
}

export const declineSharedContact = (contactId, userId) => (dispatch) => {
  Api.delete(`users/${userId}/declineshare/${contactId}`)
    .then(() => {
      dispatch(deleteContact(contactId));
    });
}

export const stopSharingContact = (contactId, userId) => (dispatch) => {
  Api.delete(`contacts/${contactId}/stopsharingwith/${userId}`)
    .then(res => {
      dispatch(updateContactAction({ ...res.data }));
    });
}