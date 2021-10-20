import { ListItem, ListItemText } from '@mui/material';
import { Link } from 'react-router-dom';

const TopNavItem = ({ title, path }) => {
  return (
    <ListItem button component={Link} style={{ width: 'fit-content' }} to={path}>
      <ListItemText primary={title} />
    </ListItem>
  );
};

export default TopNavItem;