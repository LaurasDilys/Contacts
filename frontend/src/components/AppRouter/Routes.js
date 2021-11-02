import About from '../About/About';
import ContactsProvider from '../ContactsProviders/ContactsProvider';
import RegisterForm from '../UserManager/RegisterForm';
import LoginForm from '../UserManager/LoginForm';
import MyContact from '../Profile/MyContact';
import ChangePassword from '../UserManager/ChangePassword';
import Logout from '../UserManager/Logout';

export const TopNavRoutes = [
  {
    title: "About",
    path: "/about",
    position: 'left',
    visibleLoggedIn: true,
    visibleLoggedOut: true,
    component: About
  },
  {
    title: "Contacts",
    path: "/contacts",
    position: 'left',
    visibleLoggedIn: true,
    visibleLoggedOut: false,
    component: ContactsProvider
  },
  {
    title: "Register",
    path: "/register",
    position: 'right',
    visibleLoggedIn: false,
    visibleLoggedOut: true,
    component: RegisterForm
  },
  {
    title: "Log In",
    path: "/login",
    position: 'right',
    visibleLoggedIn: false,
    visibleLoggedOut: true,
    component: LoginForm
  },
  {
    title: "My Profile",
    path: "/profile",
    position: 'right',
    visibleLoggedIn: true,
    visibleLoggedOut: false
  }
];

export const ProfileRoutes = [
  {
    title: "My Contact",
    path: "/mycontact",
    position: 'right',
    visibleLoggedIn: true,
    visibleLoggedOut: false,
    component: MyContact
  },
  {
    title: "Change Password",
    path: "/changepassword",
    position: 'right',
    visibleLoggedIn: true,
    visibleLoggedOut: false,
    component: ChangePassword
  },
  {
    title: "Log Out",
    path: "/logout",
    position: 'right',
    visibleLoggedIn: true,
    visibleLoggedOut: false,
    component: Logout
  }
];
