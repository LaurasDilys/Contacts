import { Button, Divider, TextField, Tooltip } from '@mui/material';
import AdapterDateFns from '@mui/lab/AdapterDateFns';
import LocalizationProvider from '@mui/lab/LocalizationProvider';
import DesktopDatePicker from '@mui/lab/DesktopDatePicker';
import { useEffect, useState } from 'react';
import PhoneInput from 'react-phone-input-2';
import 'react-phone-input-2/lib/material.css';
import { useDispatch, useSelector } from 'react-redux';
import { editContact, deleteContact, createContact } from '../../state/actions/contactsThunk';
import { userState } from '../../state/selectors';

const nullIfEmpty = string => {
  return string === '' || string === undefined ?
  null : string;
}

const ContactEdit = ({ contact, setEditing, creating, handleSaveNew, handleCancelNew, scrollAreaHeight }) => {
  const [firstName, setFirstName] = useState(contact.firstName);
  const [lastName, setLastName] = useState(contact.lastName);
  const [phoneNumber, setPhoneNumber] = useState(contact.phoneNumber);
  const [altPhoneNumber, setAltPhoneNumber] = useState(contact.alternativePhoneNumber);
  const [email, setEmail] = useState(contact.email);
  const [altEmail, setAltEmail] = useState(contact.alternativeEmail);
  const [dateOfBirth, setDateOfBirth] = useState(contact.dateOfBirth === null ? null : new Date(contact.dateOfBirth));
  const [dateAsString, setDateAsString] = useState();
  const [notes, setNotes] = useState(contact.notes);
  const dispatch = useDispatch();
  const { user } = useSelector(userState);

  const generateContact = () => {
    return {
      id: contact.id,
      firstName: nullIfEmpty(firstName),
      lastName: nullIfEmpty(lastName),
      phoneNumber: nullIfEmpty(phoneNumber),
      alternativePhoneNumber: nullIfEmpty(altPhoneNumber),
      email: nullIfEmpty(email),
      alternativeEmail: nullIfEmpty(altEmail),
      dateOfBirth: dateAsString,
      notes: nullIfEmpty(notes)
    };
  }
  
  useEffect(() => {
    const date = dateOfBirth?.toLocaleDateString("lt-LT");
    if (date === 'Invalid Date' || date === undefined) {
      setDateAsString(null);
    } else {
      setDateAsString(date);
    }
  }, [dateOfBirth])

  const noFirstName = () => {
    if (firstName === null || firstName === '') return true;
    return false;
  }

  const handleSave = () => {
    const contact = generateContact();

    creating ?
    dispatch(createContact(user.id, contact)) :
    dispatch(editContact(contact));
    
    creating ?
    handleSaveNew() :
    setEditing(false)
  }

  const handleCancel = () => {
    creating ?
    handleCancelNew() :
    setEditing(false)
  }

  const handleDelete = () => {
    dispatch(deleteContact(contact.id))
    setEditing(false)
  }

  return (
    <div>
      <div className='contact-area-top'>
        <Button
          onClick={handleSave}
          disabled={noFirstName()}
        >
          <span className='button-span'>Save</span>
        </Button>
        <Button onClick={handleCancel}>Cancel</Button>
        {!creating &&
        <Button color='error' onClick={handleDelete}>
          <span className='button-span'>Delete</span>
        </Button>}
        <Divider />
      </div>

      <div style={{ height: scrollAreaHeight, overflowY: 'auto', paddingRight: 80 }}>
        <Tooltip title={noFirstName() ? "First name is required" : ""}>
          <TextField
            error={noFirstName()}
            label='First Name'
            value={firstName}
            onChange={e => setFirstName(e.target.value)}
          />
        </Tooltip>

        <TextField
          label='Last Name'
          value={lastName}
          onChange={e => setLastName(e.target.value)}
        />

        <PhoneInput
          specialLabel='Phone Number'
          country='lt'
          value={phoneNumber}
          onChange={setPhoneNumber}
          masks={{lt: '(...) .....'}}
        />

        <PhoneInput
          specialLabel='Alternative Phone Number'
          country='lt'
          value={altPhoneNumber}
          onChange={setAltPhoneNumber}
          masks={{lt: '(...) .....'}}
        />

        <TextField
          label='Email'
          value={email}
          onChange={e => setEmail(e.target.value)}
        />
        <br/>
        <TextField
          label='Alternative Email'
          value={altEmail}
          onChange={e => setAltEmail(e.target.value)}
        />
        <br/>
        <LocalizationProvider dateAdapter={AdapterDateFns}>
          <DesktopDatePicker
            label='Date of Birth'
            inputFormat='yyyy/MM/dd'
            mask='____/__/__'
            value={dateOfBirth}
            onChange={setDateOfBirth}
            renderInput={(params) => <TextField {...params} />}
          />
        </LocalizationProvider>
        <br/>
        <TextField
          label="Notes"
          multiline
          rows={4}
          value={notes}
          onChange={e => setNotes(e.target.value)}
        />
      </div>
    </div>
  );
};

export default ContactEdit;