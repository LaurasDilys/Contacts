import { OTHER, SHARED, RECEIVED, UNACCEPTED, ALL } from '../../domain/contactTypes';
import KeyboardArrowDownIcon from '@mui/icons-material/KeyboardArrowDown';
import { Badge, IconButton, ListItem, ListItemIcon, ListItemText, Menu, MenuItem } from '@mui/material';
import { styled } from '@mui/system';
import { useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { contactsState } from '../../state/selectors';
import { setSelectedContactsAction } from '../../state/actions/contactsActions';
import { history } from '../AppRouter/AppRouter';
import { AccountCircle } from '@mui/icons-material';
import { ProfileRoutes } from '../AppRouter/Routes';
import { onConfirm } from '../ConfirmAlert/ConfirmAlert';

const ProfileMenu = () => {
  const [anchorElement, setAnchorElement] = useState(null);
  
  const handleNavigation = path => {
    if (path !== '/logout') {
      history.push(path);
    } else {
      onConfirm('log out', () => history.push(path));
    }
  }

  const handleOpenMenu = (event) => {
    setAnchorElement(event.currentTarget);
    event.currentTarget.blur();
  };

  const handleSelect = path => {
    handleNavigation(path);
    handleCloseMenu();
  }

  const handleCloseMenu = () => {
    setAnchorElement(null);
  };

  return (
    <>
      <ListItem
        button
        onClick={handleOpenMenu}
        style={{ width: 'fit-content' }}
      >
        <ListItemText
          primary='My Profile'
          style={{marginRight: 10}}
        />
        <AccountCircle />
      </ListItem>

      <Menu
        anchorEl={anchorElement}
        anchorOrigin={{
          vertical: 'bottom',
          horizontal: 'right',
        }}
        keepMounted
        transformOrigin={{
          vertical: 'top',
          horizontal: 'right',
        }}
        open={Boolean(anchorElement)}
        onClose={handleCloseMenu}
      >
        {ProfileRoutes.map(r => 
        <MenuItem onClick={() => handleSelect(r.path)}>
          {r.title}
        </MenuItem>)}
      </Menu>
    </>
  );
}

export default ProfileMenu;