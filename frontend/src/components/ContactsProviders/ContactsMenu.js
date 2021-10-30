import { OTHER, SHARED, RECEIVED, UNACCEPTED, ALL } from '../../domain/contactTypes';
import KeyboardArrowDownIcon from '@mui/icons-material/KeyboardArrowDown';
import { Badge, IconButton, Menu, MenuItem } from '@mui/material';
import { styled } from '@mui/system';
import { useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { contactsState } from '../../state/selectors';
import { setSelectedContactsAction } from '../../state/actions/contactsActions';
import { history } from '../AppRouter/AppRouter';

const ContactsMenu = () => {
  const { contacts: allContacts } = useSelector(contactsState);
  const [anchorElement, setAnchorElement] = useState(null);
  const dispatch = useDispatch();

  const handleOpenMenu = (event) => {
    setAnchorElement(event.currentTarget);
  };

  const handleSelect = contactType => {
    dispatch(setSelectedContactsAction(contactType));
    history.push('/contacts');
    handleCloseMenu();
  }

  const handleCloseMenu = () => {
    setAnchorElement(null);
  };

  return (
    allContacts.some(c => c.type != OTHER && c.type != null) ?
    <>
      <IconButton
        edge='start'
        color='inherit'
        style={{ width: 28, height: 28, marginTop: 10 }}
        onClick={handleOpenMenu}
      >
        <StyledBadge badgeContent={
          allContacts.filter(c => c.type == UNACCEPTED).length
        }>
          <KeyboardArrowDownIcon />
        </StyledBadge>
      </IconButton>

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
        <div>
          <MenuItem onClick={() => handleSelect(ALL)}>All Contacts</MenuItem>
          {allContacts.some(c => c.type == SHARED) &&
          <MenuItem onClick={() => handleSelect(SHARED)}>Shared</MenuItem>}
          {allContacts.some(c => c.type == RECEIVED) &&
          <MenuItem onClick={() => handleSelect(RECEIVED)}>Received</MenuItem>}
          {allContacts.some(c => c.type == UNACCEPTED) &&
          <MenuItem onClick={() => handleSelect(UNACCEPTED)}><b>Unaccepted</b></MenuItem>}
        </div>
      </Menu>
    </> :
    null
  );
}

const StyledBadge = styled(Badge)(() => ({
  '& .MuiBadge-badge': {
    right: 1,
    top: 2,
    padding: '0 3px',
    backgroundColor: '#edf4fb',
    color: '#1565c0'
  },
}));

export default ContactsMenu;