import { Button, Divider, TextField, Tooltip } from '@mui/material';
import AdapterDateFns from '@mui/lab/AdapterDateFns';
import LocalizationProvider from '@mui/lab/LocalizationProvider';
import DesktopDatePicker from '@mui/lab/DesktopDatePicker';
import { useEffect, useState } from 'react';
import PhoneInput from 'react-phone-input-2';
import 'react-phone-input-2/lib/material.css';
import { useDispatch, useSelector } from 'react-redux';
import { updateContact, deleteContact, createContact } from '../../state/actions/contactsThunk';
import { userState } from '../../state/selectors';
import { onConfirm } from '../ConfirmAlert/ConfirmAlert';
import { updateUser } from '../../state/actions/userThunk';

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
  const [address, setAddress] = useState(contact.address);
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
      address: nullIfEmpty(address),
      dateOfBirth: dateAsString,
      notes: nullIfEmpty(notes)
    };
  }

  const generateAndMakeMyContactVisible = (showMyContact) => {
    return {
      id: user.id,
      showMyContact: showMyContact,
      firstName: nullIfEmpty(firstName),
      lastName: nullIfEmpty(lastName),
      phoneNumber: nullIfEmpty(phoneNumber),
      alternativePhoneNumber: nullIfEmpty(altPhoneNumber),
      email: nullIfEmpty(email),
      alternativeEmail: nullIfEmpty(altEmail),
      address: nullIfEmpty(address),
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
    let newContactInformation;
    
    if (creating) {
      newContactInformation = generateContact();
      dispatch(createContact(user.id, newContactInformation));
    } else {
      if (contact.me) {
        newContactInformation = generateAndMakeMyContactVisible(true);
        dispatch(updateUser(newContactInformation));
      } else {
        newContactInformation = generateContact();
        dispatch(updateContact(newContactInformation));
      }
    }
    
    creating ?
    handleSaveNew() :
    setEditing(false);
  }

  const handleCancel = () => {
    creating ?
    handleCancelNew() :
    setEditing(false);
  }

  const handleDelete = () => {
    dispatch(deleteContact(contact.id));
    setEditing(false);
  }

  const handleRemoveMyContact = () => {
    const userInformation = generateAndMakeMyContactVisible(false);
    dispatch(updateUser(userInformation));
    setEditing(false);
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
        (contact.me ?
        <Button
          color='error'
          onClick={() => onConfirm(
            <>
              <p>This contact will be removed, but not deleted.</p>
              <p>You can change the visibility of <i>My Contact</i>
              <br/>in your profile settings.</p>
            </>, handleRemoveMyContact, 'Ok/Cancel')}
        >
          <span className='button-span'>Remove</span>
        </Button> :
        <Button
          color='error'
          onClick={() => onConfirm('delete this contact', handleDelete)}
        >
          <span className='button-span'>Delete</span>
        </Button>)}
        <Divider />
      </div>

      <div style={{height: scrollAreaHeight, overflowY: 'auto'}}>
        <div className='contact-edit-row'>
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
        </div>

        <div className='contact-edit-row'>
          <div>
            <PhoneInput
              inputStyle={{
                width:'250px'
              }}
              specialLabel='Phone Number'
              country='lt'
              value={phoneNumber}
              onChange={setPhoneNumber}
              masks={{lt: '(...) .....'}}
            />
          </div>
          <div>
            <PhoneInput
              inputStyle={{width:'250px'}}
              specialLabel='Alternative Phone Number'
              country='lt'
              value={altPhoneNumber}
              onChange={setAltPhoneNumber}
              masks={{lt: '(...) .....'}}
            />
          </div>
        </div>

        <div className='contact-edit-row'>
          <TextField
            className='contact-edit-field'
            label='Email'
            value={email}
            onChange={e => setEmail(e.target.value)}
          />
          <TextField
            className='contact-edit-field'
            label='Alternative Email'
            value={altEmail}
            onChange={e => setAltEmail(e.target.value)}
          />
        </div>

        <div className='contact-edit-row'>
          <TextField
            className='contact-edit-field'
            label='Address'
            value={address}
            onChange={e => setAddress(e.target.value)}
          />
          <div className='date-div date-div'>
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
          </div>
        </div>
        
        <div className='contact-edit-row notes-div'>
          <TextField
            fullWidth
            label="Notes"
            multiline
            rows={6}
            value={notes}
            onChange={e => setNotes(e.target.value)}
          />
        </div>
      </div>
    </div>
  );
};

export default ContactEdit;