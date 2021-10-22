import { Switch, Redirect, Route } from 'react-router-dom';

const RouteSwitch = ({ routes, loggedIn }) => {
  return (
    <Switch>
      {routes.map(r =>
      <Route component={r.component} exact path={r.path} key={r.path} />)}
      <Redirect from="*" to={loggedIn ? '/contacts' : '/login'} />
    </Switch>
  );
};

export default RouteSwitch;