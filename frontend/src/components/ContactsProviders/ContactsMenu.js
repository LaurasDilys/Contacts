import { OTHER, SHARED, RECEIVED, UNACCEPTED, ALL } from '../../domain/contactTypes';
import KeyboardArrowDownIcon from '@mui/icons-material/KeyboardArrowDown';
import { Badge, IconButton, Menu, MenuItem } from '@mui/material';
import { styled } from '@mui/system';
import { useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { contactsState } from '../../state/selectors';
import { setSelectedContactsAction } from '../../state/actions/contactsActions';

const ContactsMenu = () => {
  const { contacts: allContacts } = useSelector(contactsState);
  const [anchorElement, setAnchorElement] = useState(null);
  const dispatch = useDispatch();

  const openMenu = (event) => {
    setAnchorElement(event.currentTarget);
  };

  const select = contactType => {
    dispatch(setSelectedContactsAction(contactType));
    closeMenu();
  }

  const closeMenu = () => {
    setAnchorElement(null);
  };

  return (
    allContacts.some(c => c.type != OTHER) ?
    <>
      <IconButton
        edge='start'
        color='inherit'
        style={{ width: 28, height: 28, marginTop: 10 }}
        onClick={openMenu}
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
        onClose={closeMenu}
      >
        <div>
          <MenuItem onClick={() => select(ALL)}>All Contacts</MenuItem>
          {allContacts.some(c => c.type == SHARED) &&
          <MenuItem onClick={() => select(SHARED)}>Shared</MenuItem>}
          {allContacts.some(c => c.type == RECEIVED) &&
          <MenuItem onClick={() => select(RECEIVED)}>Received</MenuItem>}
          {allContacts.some(c => c.type == UNACCEPTED) &&
          <MenuItem onClick={() => select(UNACCEPTED)}><b>Unaccepted</b></MenuItem>}
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