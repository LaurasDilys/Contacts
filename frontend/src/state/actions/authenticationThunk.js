import Api from '../../domain/Api';
// import { history } from '../../components/AppRouter/AppRouter';
import { loginAction, logoutAction } from './authenticationActions';
// import { SetNotificationAction } from '../actions/notificationsActions';

export const register = () => (dispatch) => {
  Api.post('register')
    .then(() => {
      // history.push('/login');
      // dispatch(SetNotificationAction({ isOpen: true, message: response.data, type: 'success' }));
    })
    .catch((error) => {
      // dispatch(SetNotificationAction({ isOpen: true, message: error.response.data, type: 'error' }));
    });
};

export const login = () => (dispatch) => {
  Api.post('login')
    .then(res => {
      dispatch(loginAction({ ...res.data }));
      // history.push('/');
    })
    .catch((error) => {
      // dispatch(SetNotificationAction({ isOpen: true, message: error.response.data, type: 'error' }));
    });
};

export const logout = () => (dispatch) => {
  Api.post('logout')
    .then(() => {
      dispatch(logoutAction());
      // history.push('/login');
    });
};

export const onStart = () => (dispatch) => {
  newCookie().then(data => {
    if (data !== 'NotLoggedIn') {
      dispatch(loginAction({ ...data }));
    } else {
      dispatch(logoutAction());
    };
  });
};

export const newCookie = () => {
  return Api.post('newcookie').then(res => res.data);
};

export const refreshCookie = () => {
  setInterval(newCookie, 48000);
};
