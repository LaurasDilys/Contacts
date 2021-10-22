import { AppBar, Button, ListItem, ListItemText, Toolbar } from '@mui/material';
import { Box } from '@mui/system';
import TopNavItem from './TopNavItem';
import { Link } from 'react-router-dom';

const TopNav = ({ routes }) => {
  //
  // const [isLoggedIn, setIsLoggedIn] = useState(false);
  //
  
  return (
    <AppBar position="static">
      <Toolbar variant="dense">
        <Box display="flex" marginRight="auto">
          {routes.map(r => r.position === 'left' &&
          <TopNavItem title={r.title} path={r.path} key={r.path} />)}
        </Box>

        {/* // */}
        {/* <Box display="flex" marginLeft="auto" marginRight="auto">
          <ListItem button onClick={() => setIsLoggedIn(!isLoggedIn)}>
            <ListItemText primary={isLoggedIn ? 'LOGOUT' : 'LOGIN'} />
          </ListItem>
        </Box> */}
        {/* // */}

        <Box display="flex" marginLeft="auto">
          {routes.map(r => r.position === 'right' &&
          <TopNavItem title={r.title} path={r.path} key={r.path} />)}
        </Box>
      </Toolbar>
    </AppBar>
  );
};

export default TopNav;