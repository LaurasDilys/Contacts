import { Switch, Redirect, Route } from 'react-router-dom';
import LoginForm from '../LoginForm/LoginForm';
import Routes from './Routes';

const RouteSwitch = ({ routes, loggedIn }) => {
  return (
    <Switch>
      {Routes.map(r =>
      <Route component={r.component} path={r.path} key={r.path}>
        {/* // */}
        {/* <LoginForm /> */}
        {/* // */}
      </Route>)}
      {/* <Redirect from="*" to={loggedIn ? '/contacts' : '/login'} /> */}
    </Switch>
  );
};

export default RouteSwitch;