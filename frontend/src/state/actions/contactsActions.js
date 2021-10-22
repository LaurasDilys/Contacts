import * as actionTypes from './actionTypes';

export const newContactAction = props => ({
  type: actionTypes.NEW_CONTACT,
  payload: props,
});

export const editContactAction = props => ({
  type: actionTypes.EDIT_CONTACT,
  payload: props,
});

export const deleteContactAction = id => ({
  type: actionTypes.DELETE_CONTACT,
  payload: id,
});