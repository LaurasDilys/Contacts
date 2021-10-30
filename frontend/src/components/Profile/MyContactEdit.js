import { Button, Divider } from '@mui/material';
import { useRef, useState } from 'react';
import { useSelector } from 'react-redux';
import { userState } from '../../state/selectors';
import Details from '../Contacts/Details';
import Description from '../Contacts/Description';

const MyContactEdit = ({ setEditing, scrollAreaHeight }) => {
  const { user } = useSelector(userState);
  const detailsDivRef = useRef(null);

  return (
    <div>
      <div className='contact-area-top'>
        <Button onClick={() => setEditing(false)}>
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



        <Details contact={user} />

        <div className='flex-row'>
          <Description>Visibility</Description>
          <div className='contact-entry'>
            {user.showMyContact?
            <span>Included in All Contacts</span> :
            <span>Hidden from All Contacts</span>}
          </div>
        </div>


        
      </div>
    </div>
  );
}

export default MyContactEdit;