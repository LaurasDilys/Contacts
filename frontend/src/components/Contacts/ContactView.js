import './Contacts.css'
import { Avatar, Button, Divider, TextField } from "@mui/material";
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

const ContactView = ({ contact, setEditing }) => {
  const [formattedNumber, setFormattedNumber] = useState();
  const phoneInputRef = useRef(null);

  useEffect(() => {
    const timeout = setTimeout(() => {
      setFormattedNumber(phoneInputRef?.current?.state.formattedNumber);
    }, 1); // it takes some time for PhoneInput to calculate formattedNumber
    return () => clearTimeout(timeout);
  }, [contact])

  return (
    <div>
      <div className='contact-area-top'>
        <Button onClick={() => setEditing(true)}>Edit</Button>
        <Divider />
      </div>

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

      {contact.email?.length > 0 &&
      <div className='flex-row'>
        <Description>Email</Description>
        <div className='contact-entry'>
          <a href={`mailto:${contact.email}`}>
            {contact.email}
          </a>
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
  );
};

export default ContactView;