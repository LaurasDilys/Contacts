import { useEffect, useState } from 'react';
import { Router, Switch, Route } from 'react-router-dom';
import { createBrowserHistory } from 'history';
import TopNav from './TopNav';
import RouteSwitch from './RouteSwitch';
import { useSelector } from 'react-redux';
import { userState } from '../../state/selectors';
import Routes from './Routes';

export const history = createBrowserHistory();

const AppRouter = () => {
  const { loggedIn } = useSelector(userState);
  const [availableRoutes, setAvailableRoutes] = useState([]);

  useEffect(() => {
    loggedIn ?
    setAvailableRoutes(Routes.filter(r => r.visibleLoggedIn)) :
    setAvailableRoutes(Routes.filter(r => r.visibleLoggedOut));
  }, [loggedIn])

  return (
    <Router history={history}>
      <TopNav routes={Routes} />
      {/* <Notification /> */}
      {/* <RouteSwitch routes={Routes} loggedIn={loggedIn} /> */}
      <Switch>
        {Routes.map(r =>
        <Route component={r.component} path={r.path} key={r.path}>
        </Route>)}
      </Switch>
    </Router>
  );
};

export default AppRouter;
