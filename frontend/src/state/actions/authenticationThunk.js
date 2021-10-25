import Api from '../../domain/Api';
import { history } from '../../components/AppRouter/AppRouter';
import { loginAction, logoutAction } from './authenticationActions';
import { getContacts } from './contactsThunk';
// import { SetNotificationAction } from '../actions/notificationsActions';

export const register = (request) => () => {
  Api.post('register', request)
    .then(() => {
      history.push('/login');
      // dispatch(SetNotificationAction({ isOpen: true, message: response.data, type: 'success' }));
    })
    .catch((error) => {
      // dispatch(SetNotificationAction({ isOpen: true, message: error.response.data, type: 'error' }));
    });
}

export const login = (request) => (dispatch) => {
  Api.post('login', request)
    .then(res => {
      dispatch(loginAction({ ...res.data }));
      dispatch(getContacts(res.data.id));
    })
    .catch((error) => {
      // dispatch(SetNotificationAction({ isOpen: true, message: error.response.data, type: 'error' }));
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
  setInterval(newCookie, 48000);
}

export const checkLoginStatus = () => (dispatch) => {
  loginStatus().then(data => {
    if (data !== 'NotLoggedIn') {
      dispatch(loginAction({ ...data }));
      dispatch(getContacts(data.id));
    } else {
      dispatch(logoutAction());
    }
    setRefreshCookieInterval();
  });
}

export const logout = () => (dispatch) => {
  Api.post('logout')
    .then(() => {
      dispatch(logoutAction());
    });
}
