import React from 'react';
import { Router } from 'react-router-dom';
import { createBrowserHistory } from 'history';

export const history = createBrowserHistory();

const PageRouter = () => {
  return (
    <Router history={history}>
      {/* <TopNav />
      <Notification />
      <AllRoutes /> */}
    </Router>
  );
};

export default PageRouter;
