import Contacts from "../Contacts/Contacts";
import LoginForm from "../LoginForm/LoginForm"
import RegisterForm from "../RegisterForm/RegisterForm";

const Routes = [
  {
    title: "Home",
    path: "/home",
    position: 'left',
    visibleLoggedIn: true,
    visibleLoggedOut: true,
    component: Contacts
  },
  {
    title: "Contacts",
    path: "/contacts",
    position: 'left',
    visibleLoggedIn: true,
    visibleLoggedOut: false,
    component: Contacts
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
    title: "Log Out",
    path: "/logout",
    position: 'right',
    visibleLoggedIn: true,
    visibleLoggedOut: false,
    component: () => {return <h1>Log Out</h1>}
  },
  
]

export default Routes;