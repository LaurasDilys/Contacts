import { UserState } from './reducers/userReducer';
import { RootState } from './reducers/reducers';
import { ContactsState } from './reducers/contactsReducer';

export const userState = (state: RootState): UserState => state.user;
export const contactsState = (state: RootState): ContactsState => state.contacts;
