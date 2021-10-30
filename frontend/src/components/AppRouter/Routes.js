import ContactsProvider from "../ContactsProviders/ContactsProvider";
import LoginForm from "../LoginForm/LoginForm"
import Logout from "../Logout/Logout";
import RegisterForm from "../RegisterForm/RegisterForm";

export const TopNavRoutes = [
  {
    title: "Home",
    path: "/home",
    position: 'left',
    visibleLoggedIn: true,
    visibleLoggedOut: true,
    component: () => {return <h1>Home</h1>}
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
    component: () => {return <h1>My Contact</h1>}
  },
  {
    title: "Change Password",
    path: "/changepassword",
    position: 'right',
    visibleLoggedIn: true,
    visibleLoggedOut: false,
    component: () => {return <h1>Change Password</h1>}
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
