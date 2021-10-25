import { ListItem, ListItemText } from '@mui/material';
import { onConfirm } from '../ConfirmAlert/ConfirmAlert';
import { history } from './AppRouter';

const TopNavItem = ({ title, path }) => {

  const navigateToPath = () => history.push(path)
  
  const handleNavigation = () => {
    if (path !== '/logout') {
      navigateToPath();
    } else {
      onConfirm('log out', navigateToPath);
    }
  }

  return (
    <ListItem
      button
      onClick={handleNavigation}
      style={{ width: 'fit-content' }}
    >
      <ListItemText primary={title} />
    </ListItem>
  );
}

export default TopNavItem;