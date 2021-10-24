import './Contacts.css'
import { Avatar, Button, Divider, Input, InputAdornment, List, ListItem, ListItemText, TextField } from "@mui/material";
import AddIcon from '@mui/icons-material/Add';
import PersonSearchIcon from '@mui/icons-material/PersonSearch';
import PhoneInput from 'react-phone-input-2';
import { useEffect, useRef, useState } from 'react';
import Description from './Description';

const stringToColor = string => {
  let hash = 0;
  let i;

  for (i = 0; i < string.length; i++) {
    hash = string.charCodeAt(i) + ((hash << 5) - hash);
  }

  let color = '#';

  for (i = 0; i < 3; i++) {
    const value = (hash >> (i * 8)) & 0xff;
    color += `00${value.toString(16)}`.substr(-2);
  }

  return color;
}

const initialsAvatar = (firstName, lastName) => {
  let initials = '';
  firstName?.length > 0 && (initials += firstName[0]);
  lastName?.length > 0 && (initials += lastName[0]);

  return {
    sx: {
      bgcolor: stringToColor(`${firstName} ${lastName}`),
      width: 80,
      height: 80,
      fontSize: 40
    },
    children: initials,
  };
}

const searchFieldStyle = {
  width: 300,
  marginTop: 1,
};

const usersListStyle = {
  width: 300,
  bgcolor: 'background.paper',
};

//
//
//
const mockUsers = [
  {
    id: 'a',
    userName: '420bleizit',
    firstName: 'Arnas',
    lastName: 'B.',
    selected: false
  },
  {
    id: 'b',
    userName: 'asBenasLOL',
    firstName: 'Benas',
    lastName: 'C.',
    selected: false
  },
  {
    id: 'c',
    userName: 'TheRealCicinas',
    firstName: 'Cicinas',
    lastName: null,
    selected: false
  },
  {
    id: 'd',
    userName: 'den_den',
    firstName: 'Denas',
    lastName: 'F.',
    selected: false
  }
]
//
//
//
const noneAreSelected = array => {
  return array.some(item => item.selected) ? false : true;
}
//
//
//

const ContactView = ({ contact, setEditing, handleNew, scrollAreaHeight, scrollBarWidth }) => {
  const [formattedNumber, setFormattedNumber] = useState();
  const [formattedAltNumber, setFormattedAltNumber] = useState();
  const [sharing, setSharing] = useState(false);
  //
  const [users, setUsers] = useState(mockUsers);
  //
  const phoneInputRef = useRef(null);
  const altPhoneInputRef = useRef(null);

  useEffect(() => {
    const timeout = setTimeout(() => {
      setFormattedNumber(phoneInputRef?.current?.state.formattedNumber);
      setFormattedAltNumber(altPhoneInputRef?.current?.state.formattedNumber);
    }, 1); // it takes some time for PhoneInput to calculate formattedNumber
    return () => clearTimeout(timeout);
  }, [contact])

  const scrollX = options => {
    const timeout = setTimeout(() => {
      window.scrollBy(options);
    }, 1); // it takes some time for contacts list to get rendered
    return () => clearTimeout(timeout);
  }

  return (
    <div className='flex-row'>
      <div className='contact-area'>
        <div className='contact-area-top'>
          {sharing?
          <div>
            <Button onClick={() => {
              // if x axis overflows
              scrollX({ left: -1000 });
              setSharing(false);
            }}>
              {/* change to: handle cancel share */}
              <span className='button-span'>Cancel</span>
            </Button>
            <Button disabled={noneAreSelected(users)}>
              <span className='button-span'>Share Contact</span>
            </Button>
          </div> :
          <div>
            <Button onClick={handleNew}>
              <AddIcon sx={{ marginBottom: '2px' }} />
              <span className='button-span'>New</span>
            </Button>
            <Button onClick={() => setEditing(true)}>
              <span className='button-span'>Edit</span>
            </Button>
            <Button onClick={() => {
              // if x axis overflows
              scrollX({ left: 1000, behavior: 'smooth' });
              setSharing(true);
            }}>
              <span className='button-span'>Share</span>
            </Button>
          </div>}
          <Divider />
        </div>

        <div style={{ height: scrollAreaHeight, overflowY: 'auto', paddingRight: 80 }}>
          <div className='flex-row'>
            <div className='avatar-div-row'>
              <div className='avatar-div-col'>
                <Avatar {...initialsAvatar(contact.firstName, contact.lastName)} />
              </div>
            </div>
            <div className='name-div'>
              <h1>{contact.firstName} {contact.lastName}</h1>
            </div>
          </div>

          {contact.phoneNumber?.length > 0 &&
          <div className='flex-row'>
            <Description>Phone Number</Description>
            <div className='contact-entry'>
              <a href={`tel:+${contact.phoneNumber}`}>
                {formattedNumber}
              </a>
              <PhoneInput
                ref={phoneInputRef}
                style={{ display: 'none' }}
                value={contact.phoneNumber}
                masks={{lt: '(...) .....'}}
              />
            </div>
          </div>}

          {contact.alternativePhoneNumber?.length > 0 &&
          <div className='flex-row'>
            <Description>Alternative Phone Number</Description>
            <div className='contact-entry'>
              <a href={`tel:+${contact.alternativePhoneNumber}`}>
                {formattedAltNumber}
              </a>
              <PhoneInput
                ref={altPhoneInputRef}
                style={{ display: 'none' }}
                value={contact.alternativePhoneNumber}
                masks={{lt: '(...) .....'}}
              />
            </div>
          </div>}

          {contact.email?.length > 0 &&
          <div className='flex-row'>
            <Description>Email</Description>
            <div className='contact-entry'>
              <a href={`mailto:${contact.email}`}>
                {contact.email}
              </a>
            </div>
          </div>}

          {contact.alternativeEmail?.length > 0 &&
          <div className='flex-row'>
            <Description>Alternative Email</Description>
            <div className='contact-entry'>
              <a href={`mailto:${contact.alternativeEmail}`}>
                {contact.alternativeEmail}
              </a>
            </div>
          </div>}

          {contact.dateOfBirth?.length > 0 &&
          <div className='flex-row'>
            <Description>Date of Birth</Description>
            <div className='contact-entry'>
              <span>{contact.dateOfBirth}</span>
            </div>
          </div>}

          {contact.notes?.length > 0 &&
          <div className='flex-row'>
            <Description>Notes</Description>
            <div className='contact-entry'>
              <span>{contact.notes}</span>
            </div>
          </div>}
        </div>
      </div>
      
      {sharing &&
      <div className='users-area'>
        <Input
          sx={searchFieldStyle}
          // value={search}
          // onChange={handleSearch}
          placeholder='Search Users'
          startAdornment={
            <InputAdornment position='start' style={{marginLeft: 12}}>
              <PersonSearchIcon />
            </InputAdornment>}
        />
        <List
          sx={usersListStyle}
          style={{ height: scrollAreaHeight - scrollBarWidth, overflowY: 'auto' }}
          // ref={usersListRef}
        >
        {/* filteredUsers */}
          {users.map(u =>
          <div key={u.id}>
            <ListItem selected={u.selected} /*onClick={() => handleSelect(u.id)}*/>
                                          {/* filteredUsers */}
              <ListItemText primary={`${u.firstName} ${u.lastName}`} secondary={u.userName} />
            </ListItem>
            <Divider />
          </div>)}
        </List>
      </div>}
    </div>
  );
};

export default ContactView;