import { Button, Divider } from '@mui/material';
import { useState } from 'react';
import { useSelector } from 'react-redux';
import { userState } from '../../state/selectors';
import Details from '../Contacts/Details';
// import './Profile.css';

const MyContact = () => {
  const [editing, setEditing] = useState(false);
  const { user } = useSelector(userState);

  return (
    user === null ?
    null :
    <div className='flex-row center'>
      {editing ?
      null:
      <MyContactView />}
    </div>
  );
}

const MyContactView = ({  }) => {
  const { user } = useSelector(userState);

  return (
    <div>
      <div className='contact-area-top'>
        <Button
          // onClick={handleSave}
          // disabled={noFirstName()}
        >
          <span className='button-span'>Edit</span>
        </Button>

        <Divider />
      </div>

      <Details contact={user} />

    </div>
  );
}

export default MyContact;