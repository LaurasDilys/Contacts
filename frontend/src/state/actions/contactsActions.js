import * as actionTypes from './actionTypes';

export const getContactsAction = props => ({
  type: actionTypes.GET_CONTACTS,
  payload: props
})

export const createContactAction = props => ({
  type: actionTypes.NEW_CONTACT,
  payload: props
});

export const updateContactAction = props => ({
  type: actionTypes.UPDATE_CONTACT,
  payload: props
});

export const deleteContactAction = id => ({
  type: actionTypes.DELETE_CONTACT,
  payload: id
});

export const setSelectedContactsAction = props => ({
  type: actionTypes.SET_SELECTED_CONTACTS,
  payload: props
});

export const updateMyContactAction = props => ({
  type: actionTypes.UPDATE_MY_CONTACT,
  payload: props
});