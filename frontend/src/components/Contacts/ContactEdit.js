import { Button, Divider, TextField, Tooltip } from "@mui/material";
import AdapterDateFns from '@mui/lab/AdapterDateFns';
import LocalizationProvider from '@mui/lab/LocalizationProvider';
import DesktopDatePicker from '@mui/lab/DesktopDatePicker';
import { useEffect, useState } from "react";
import PhoneInput from "react-phone-input-2";
import 'react-phone-input-2/lib/material.css';

const contact = {
  id: 0,
  firstName: 'Lauras',
  lastName: 'Dilys',
  phoneNumber: '37061417706',
  alternativePhoneNumber: '37061536904',
  email: 'lauras.dilys@gmail.com',
  alternativeEmail: 'spotas@gmail.com',
  dateOfBirth: null,
  notes: 'This is my contact'
}

const ContactEdit = ({  setEditing }) => {
  const [firstName, setFirstName] = useState(contact.firstName);
  const [lastName, setLastName] = useState(contact.lastName);
  const [phoneNumber, setPhoneNumber] = useState(contact.phoneNumber);
  const [altPhoneNumber, setAltPhoneNumber] = useState(contact.alternativePhoneNumber);
  const [email, setEmail] = useState(contact.email);
  const [altEmail, setAltEmail] = useState(contact.alternativeEmail);
  const [dateOfBirth, setDateOfBirth] = useState(contact.dateOfBirth === null ? null : new Date(contact.dateOfBirth));
  const [dateAsString, setDateAsString] = useState();
  const [notes, setNotes] = useState(contact.notes);


  //
  // useEffect(() => {
  //   const DATE_AS_STRING = dateOfBirth?.toLocaleDateString("lt-LT");
  //   console.log(DATE_AS_STRING === 'Invalid Date'
  //   || DATE_AS_STRING === undefined) // dateOfBirth = null;
  // }, [dateOfBirth])
  //


  const noFirstName = () => {
    if (firstName === null || firstName === '') return true;
    return false;
  }

  return (
    <div>
      <div className='contact-area-top'>
        <Button onClick={() => setEditing(false)}>Cancel</Button>
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