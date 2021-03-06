import { UserState } from './reducers/userReducer';
import { RootState } from './reducers/reducers';
import { ContactsState } from './reducers/contactsReducer';
import { OtherUsersState } from './reducers/otherUsersReducer';
import { NotificationState } from './reducers/notificationReducer';

export const userState = (state: RootState): UserState => state.user;
export const contactsState = (state: RootState): ContactsState => state.contacts;
export const otherUsersState = (state: RootState): OtherUsersState => state.otherUsers;
export const notificationState = (state: RootState): NotificationState => state.notification;