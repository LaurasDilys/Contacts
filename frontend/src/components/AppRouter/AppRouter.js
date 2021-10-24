import { useEffect, useState } from 'react';
import { Router } from 'react-router-dom';
import { createBrowserHistory } from 'history';
import TopNav from './TopNav';
import RouteSwitch from './RouteSwitch';
import { useSelector } from 'react-redux';
import { userState } from '../../state/selectors';
import Routes from './Routes';
import { Toolbar } from '@mui/material';

export const history = createBrowserHistory();

const AppRouter = () => {
  const appBarHeight = 48;
  const { loggedIn } = useSelector(userState);
  const [availableRoutes, setAvailableRoutes] = useState([]);

  useEffect(() => {
    loggedIn ?
    setAvailableRoutes(Routes.filter(r => r.visibleLoggedIn)) :
    setAvailableRoutes(Routes.filter(r => r.visibleLoggedOut));
  }, [loggedIn])

  return (
    <Router history={history} style={{ overflowY: 'visible' }}>
      <div style={{ height: appBarHeight }} />
      <TopNav // empty div above corrects for nav height
        position='fixed' // nav stays fixed when x axis overflows
        height={appBarHeight} // making sure that nav and div above have the same height
        routes={availableRoutes}
      />
      {/* <Notification /> */}
      <RouteSwitch routes={availableRoutes} loggedIn={loggedIn} />
    </Router>
  );
};

export default AppRouter;
