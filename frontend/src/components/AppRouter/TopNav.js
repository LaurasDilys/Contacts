import { AppBar, Toolbar } from '@mui/material';
import { Box } from '@mui/system';
import TopNavItem from './TopNavItem';

const TopNav = ({ position, height, routes }) => {
  return (
    <AppBar position={position} style={{ height: height }}>
      <Toolbar variant="dense">
        <Box display="flex" marginRight="auto">
          {routes.map(r => r.position === 'left' &&
          <TopNavItem title={r.title} path={r.path} key={r.path} />)}
        </Box>
        <Box display="flex" marginLeft="auto">
          {routes.map(r => r.position === 'right' &&
          <TopNavItem title={r.title} path={r.path} key={r.path} />)}
        </Box>
      </Toolbar>
    </AppBar>
  );
};

export default TopNav;