import { Button, Divider, TextField, Tooltip } from "@mui/material";
import AdapterDateFns from '@mui/lab/AdapterDateFns';
import LocalizationProvider from '@mui/lab/LocalizationProvider';
import DesktopDatePicker from '@mui/lab/DesktopDatePicker';
import { useEffect, useState } from "react";
import PhoneInput from "react-phone-input-2";
import 'react-phone-input-2/lib/material.css';

// const Lauras = {
//   id: 0,
//   firstName: 'Lauras',
//   lastName: 'Dilys',
//   phoneNumber: '37061417706',
//   alternativePhoneNumber: '37061536904',
//   email: 'lauras.dilys@gmail.com',
//   alternativeEmail: 'spotas@gmail.com',
//   dateOfBirth: null,
//   notes: 'This is my contact'
// }

const nullIfEmpty = string => {
  return string === '' || string === undefined ?
  null : string;
}

const ContactEdit = ({ contact, setEditing, newContact, handleSaveNew, handleCancelNew }) => {
  const [firstName, setFirstName] = useState(contact.firstName);
  const [lastName, setLastName] = useState(contact.lastName);
  const [phoneNumber, setPhoneNumber] = useState(contact.phoneNumber);
  const [altPhoneNumber, setAltPhoneNumber] = useState(contact.alternativePhoneNumber);
  const [email, setEmail] = useState(contact.email);
  const [altEmail, setAltEmail] = useState(contact.alternativeEmail);
  const [dateOfBirth, setDateOfBirth] = useState(contact.dateOfBirth === null ? null : new Date(contact.dateOfBirth));
  const [dateAsString, setDateAsString] = useState();
  const [notes, setNotes] = useState(contact.notes);

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
    //
    console.log(contact);
    //
    // newContact ?
    //
    // dispatch(newContact(contact)) :
    //
    // if saving new contact, it needs to be set as selected
    // this will have to be done as part of CONTACTS ACTIONS
    //
    // dispatch(patchContact(contact))
    //
    newContact ?
    handleSaveNew() :
    setEditing(false)
  }

  const handleCancel = () => {
    newContact ?
    handleCancelNew() :
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
        {/* // */}
        {/* NOT IMPLEMENTED */}
        {!newContact &&
        <Button color='error'>
          <span className='button-span'>Delete</span>
        </Button>}
        {/* NOT IMPLEMENTED */}
        {/* // */}
        <Divider />
      </div>

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
          label='Date Of Birth'
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
  );
};

export default ContactEdit;