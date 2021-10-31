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
    event.currentTarget.blur();
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
          <MenuItem
            disabled={!allContacts.some(c => c.type === SHARED)}
            onClick={() => handleSelect(SHARED)}
          >
            Shared
          </MenuItem>
          <MenuItem
            disabled={!allContacts.some(c => c.type === RECEIVED)}
            onClick={() => handleSelect(RECEIVED)}
          >
            Received
          </MenuItem>

          <MenuItem
            disabled={!allContacts.some(c => c.type == UNACCEPTED)}
            onClick={() => handleSelect(UNACCEPTED)}
          >
            {!allContacts.some(c => c.type == UNACCEPTED) ?
            'Unaccepted' :
            <b>Unaccepted</b>}
          </MenuItem>
        </div>
      </Menu>
    </>
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