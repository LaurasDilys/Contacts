import { Button, Divider, FormControlLabel, TextField, Tooltip } from '@mui/material';
import AdapterDateFns from '@mui/lab/AdapterDateFns';
import LocalizationProvider from '@mui/lab/LocalizationProvider';
import DesktopDatePicker from '@mui/lab/DesktopDatePicker';
import { useEffect, useRef, useState } from 'react';
import PhoneInput from 'react-phone-input-2';
import 'react-phone-input-2/lib/material.css';
import { useDispatch, useSelector } from 'react-redux';
import { updateContact, deleteContact, createContact } from '../../state/actions/contactsThunk';
import { userState } from '../../state/selectors';
import { onConfirm } from '../ConfirmAlert/ConfirmAlert';
import { updateUser } from '../../state/actions/userThunk';
import { nullIfEmpty } from '../Contacts/ContactEdit';
import { MyContactSwitch } from './MyContact';
import './Profile.css';

const MyContactEdit = ({ setEditing, creating, handleSaveNew, handleCancelNew, scrollAreaHeight }) => {
  const { user } = useSelector(userState);
  const [firstName, setFirstName] = useState(user.firstName);
  const [lastName, setLastName] = useState(user.lastName);
  const [phoneNumber, setPhoneNumber] = useState(user.phoneNumber);
  const [altPhoneNumber, setAltPhoneNumber] = useState(user.alternativePhoneNumber);
  const [email, setEmail] = useState(user.email);
  const [altEmail, setAltEmail] = useState(user.alternativeEmail);
  const [address, setAddress] = useState(user.address);
  const [dateOfBirth, setDateOfBirth] = useState(user.dateOfBirth === null ? null : new Date(user.dateOfBirth));
  const [dateAsString, setDateAsString] = useState();
  const [notes, setNotes] = useState(user.notes);
  const [showMyContact, setShowContact] = useState(user.showMyContact);
  const detailsDivRef = useRef(null);
  const dispatch = useDispatch();

  const generateUser = () => {
    return {
      id: user.id,
      firstName: nullIfEmpty(firstName),
      lastName: nullIfEmpty(lastName),
      phoneNumber: nullIfEmpty(phoneNumber),
      alternativePhoneNumber: nullIfEmpty(altPhoneNumber),
      email: nullIfEmpty(email),
      alternativeEmail: nullIfEmpty(altEmail),
      address: nullIfEmpty(address),
      dateOfBirth: dateAsString,
      notes: nullIfEmpty(notes),
      showMyContact: showMyContact
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

  const noLastName = () => {
    if (lastName === null || lastName === '') return true;
    return false;
  }

  const handleSave = () => {
    const newInformation = generateUser(true);
    dispatch(updateUser(newInformation));
    setEditing(false);
  }

  return (
    <div>
      <div className='contact-area-top'>
        <Button
          onClick={handleSave}
          disabled={noFirstName() || noLastName()}
        >
          <span className='button-span'>Save</span>
        </Button>
        <Button onClick={() => setEditing(false)}>
          <span className='button-span'>Cancel</span>
        </Button>
        <Divider />
      </div>

      <div
        ref={detailsDivRef}
        style={{
          height: scrollAreaHeight - detailsDivRef.current?.offsetTop,
          overflowY: 'auto'
        }}
      >
        <div className='contact-edit-row'>
          <Tooltip title={noFirstName() ? "First name is required" : ""}>
            <TextField
              error={noFirstName()}
              label='First Name'
              value={firstName}
              onChange={e => setFirstName(e.target.value)}
            />
          </Tooltip>
          <Tooltip title={noLastName() ? "Last name is required" : ""}>
            <TextField
              error={noLastName()}
              label='Last Name'
            value={lastName}
            onChange={e => setLastName(e.target.value)}
            />
          </Tooltip>
        </div>

        <div className='contact-edit-row'>
          <div>
            <PhoneInput
              inputStyle={{width:'250px'}}
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
        
        <div className='contact-edit-row user-notes-div'>
          <TextField
            fullWidth
            label="Notes"
            multiline
            rows={6}
            value={notes}
            inputProps={{ maxLength: 1000 }}
            onChange={e => setNotes(e.target.value)}
          />
        </div>

        <div className='switch-div'>
          <FormControlLabel
            control={
              <MyContactSwitch
                checked={showMyContact}
                onChange={e => setShowContact(e.target.checked)}
              />
            }
            label={
              <span className='switch-label'>Include in All Contacts</span>
            }
          />
        </div>
      </div>
    </div>
  );
};

export default MyContactEdit;