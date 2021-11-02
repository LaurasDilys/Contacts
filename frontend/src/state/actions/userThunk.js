import Api from '../../domain/Api';
import { history } from '../../components/AppRouter/AppRouter';
import { loginAction, logoutAction, updateUserInformationAction } from './userActions';
import { getContacts } from './contactsThunk';
import { getOtherUsers } from './otherUsersThunk';
import { updateMyContactAction } from './contactsActions';
import { actionFrom, setNotification } from '../actions/notificationActions';

export const register = (request) => (dispatch) => {
  Api.post('register', request)
    .then(res => {
      history.push('/login');
      dispatch(setNotification(actionFrom('success', res)));
    })
    .catch(err => {
      dispatch(setNotification(actionFrom('error', err)));
    });
}

export const login = (request) => (dispatch) => {
  Api.post('login', request)
    .then(res => {
      dispatch(loginAction({ ...res.data }));
      dispatch(getContacts(res.data.id));
      dispatch(getOtherUsers(res.data.id));
    })
    .catch(err => {
      dispatch(setNotification(actionFrom('error', err)));
    });
}

const loginStatus = () => {
  return Api.post('loginstatus').then(res => res.data);
}

const newCookie = () => {
  return Api.post('newcookie')
    .catch(err => console.log('Login to continue.'));
}

const setRefreshCookieInterval = () => {
  setInterval(newCookie, 880000);
}

export const checkLoginStatus = () => (dispatch) => {
  loginStatus()
    .then(data => {
      if (data !== 'NotLoggedIn') {
        dispatch(loginAction({ ...data }));
        dispatch(getContacts(data.id));
        dispatch(getOtherUsers(data.id));
      } else {
        dispatch(logoutAction());
      }
      setRefreshCookieInterval();
    })
    .catch(err => {
      dispatch(setNotification(actionFrom('error', err)));
    });
}

export const logout = () => (dispatch) => {
  Api.post('logout')
    .then(() => {
      dispatch(logoutAction());
    });
}

export const updateUser = (request) => (dispatch) => {
  Api.put(`users/${request.id}`, request)
    .then(res => {
      dispatch(updateMyContactAction( res.data.myContact ));
      dispatch(updateUserInformationAction( res.data.user ));
    });
}

export const changePassword = (id, request) => (dispatch) => {
  Api.post(`users/${id}/changepassword`, request)
    .then(res => {
      dispatch(setNotification(actionFrom('success', res)));
    })
    .catch(err => {
      dispatch(setNotification(actionFrom('error', err)));
    });
}