import { useEffect, useState } from 'react';
import { Router } from 'react-router-dom';
import { createBrowserHistory } from 'history';
import TopNav from './TopNav';
import RouteSwitch from './RouteSwitch';
import { useSelector } from 'react-redux';
import { userState } from '../../state/selectors';
import { ProfileRoutes, TopNavRoutes } from './Routes';
import Notification from '../Notification/Notification';

export const history = createBrowserHistory();

const AppRouter = () => {
  const appBarHeight = 48;
  const { loggedIn } = useSelector(userState);
  const [availableTopNavRoutes, setAvailableTopNavRoutes] = useState([]);
  const [availableRoutes, setAvailableRoutes] = useState([]);

  useEffect(() => {
    loggedIn ?
    setAvailableTopNavRoutes(TopNavRoutes.filter(r => r.visibleLoggedIn)) :
    setAvailableTopNavRoutes(TopNavRoutes.filter(r => r.visibleLoggedOut));
  }, [loggedIn])

  useEffect(() => {
    const allRoutes = [
      ...TopNavRoutes.filter(r => r.path !== '/profile'),
      ...ProfileRoutes
    ];

    loggedIn ?
    setAvailableRoutes(allRoutes.filter(r => r.visibleLoggedIn)) :
    setAvailableRoutes(allRoutes.filter(r => r.visibleLoggedOut));
  }, [loggedIn])

  return (
    <Router history={history} style={{ overflowY: 'visible' }}>
      <div style={{ height: appBarHeight }} />
      <TopNav // empty div above corrects for nav height
        position='fixed' // nav stays fixed when x axis overflows
        height={appBarHeight} // making sure that nav and div above have the same height
        routes={availableTopNavRoutes}
      />
      <Notification />
      <RouteSwitch routes={availableRoutes} loggedIn={loggedIn} />
    </Router>
  );
};

export default AppRouter;
