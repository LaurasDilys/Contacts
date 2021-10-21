import './Contacts.css'
import { Avatar, Button, Divider, TextField } from "@mui/material";

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
  return {
    sx: {
      bgcolor: stringToColor(`${firstName} ${lastName}`),
      width: 80,
      height: 80,
      fontSize: 40
    },
    children:
    `${firstName.length > 0 && firstName[0]}${lastName.length > 0 && lastName[0]}`,
  };
}

const ContactView = ({ contact, setEditing }) => {

  return (
    <div>
      <div className='contact-area-top'>
        <Button onClick={() => setEditing(true)}>Edit</Button>
        <Divider />
      </div>

      <div className='flex-row'>
        <Avatar {...initialsAvatar(contact.firstName, contact.lastName)} />
        <h1>{contact.firstName} {contact.lastName}</h1>
      </div>

      <div className='flex-row'>
        <p>Phone Number</p>
        <TextField
          defaultValue={contact.phoneNumber}
          variant='standard'
          InputProps={{
            readOnly: true,
          }}
        />
      </div>

      {/* phoneNumber: '37061417706',
  email: 'lauras.dilys@gmail.com',
  notes: 'This is my contact', */}
      
    </div>
  );
};

export default ContactView;