import './Contacts.css'
import { Button, Divider } from "@mui/material";
import AddIcon from '@mui/icons-material/Add';
import { useEffect, useState } from 'react';
import Description from './Description';
import UsersList from '../Users/UsersList';
import { useDispatch, useSelector } from 'react-redux';
import { otherUsersState, userState } from '../../state/selectors';
import { acceptSharedContact, declineSharedContact, shareContact } from '../../state/actions/contactsThunk';
import UserChip from '../Users/UserChip';
import { RECEIVED, UNACCEPTED } from '../../domain/contactTypes';
import { onConfirm } from '../ConfirmAlert/ConfirmAlert';
import Details from './Details';

export const stringToColor = string => {
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

const ContactView = ({ contact, setEditing, handleNew, scrollAreaHeight, scrollBarWidth }) => {
  const [sharing, setSharing] = useState(false);
  const { user } = useSelector(userState);
  const { otherUsers } = useSelector(otherUsersState);
  const [filteredUsers, setFilteredUsers] = useState([]);
  const [selectedUserId, setSelectedUserId] = useState(null);
  const dispatch = useDispatch();

  useEffect(() => {
    setSharing(false); // cancel sharing, when a different user is selected
    otherUsers.forEach(user => user.selected = false);
    if (contact.type !== RECEIVED && contact.type !== UNACCEPTED) {
      // can't share received contacts (neither accepted, nor unaccepted)
      if (contact.sharedWith === null) {
        setFilteredUsers(otherUsers);
      } else {
        // can't share with who's already being shared with
        const sharedIds = contact.sharedWith.map(user => user.id);
        setFilteredUsers(otherUsers.filter(user => !sharedIds.includes(user.id)));
      }
    } else {
      setFilteredUsers([]);
    }
  }, [contact, otherUsers])

  const scrollX = options => {
    const timeout = setTimeout(() => {
      window.scrollBy(options);
    }, 1); // it takes some time for contacts list to get rendered
    return () => clearTimeout(timeout);
  }

  const handleShare = () => {
    dispatch(shareContact(contact.id, selectedUserId));
    setSharing(false);
  }

  const handleAcceptShare = () => {
    dispatch(acceptSharedContact(contact.id, user.id));
  }

  const handleDeclineShare = () => {
    dispatch(declineSharedContact(contact.id, user.id));
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
              <span className='button-span'>Cancel</span>
            </Button>
            <Button
              disabled={selectedUserId === null}
              onClick={handleShare}
            >
              <span className='button-span'>Share Contact</span>
            </Button>
          </div> :
          <div>
            <Button onClick={handleNew}>
              <AddIcon sx={{ marginBottom: '2px' }} />
              <span className='button-span'>New</span>
            </Button>
            {(contact.type !== RECEIVED &&
            contact.type !== UNACCEPTED) &&
            <>
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
            </>}
            {contact.type === UNACCEPTED &&
            <>
              <Button
                color='success'
                onClick={handleAcceptShare}
              >
                <span className='button-span'>Accept</span>
              </Button>
              <Button
                color='error'
                onClick={() => onConfirm('decline', handleDeclineShare)}
              >
                <span className='button-span'>Decline</span>
              </Button>
            </>}
            {contact.type === RECEIVED &&
            <Button
              color='error'
              onClick={() => onConfirm('remove this contact', handleDeclineShare)}
            >
              <span className='button-span'>Remove</span>
            </Button>}
          </div>}
          <Divider />
        </div>

        <div style={{ height: scrollAreaHeight, overflowY: 'auto', paddingRight: 80 }}>
          <Details contact={contact} />

          {contact.receivedFrom &&
          <div className='flex-row'>
            <Description>Received From</Description>
            <div className='contact-entry'>
              <UserChip
                user={contact.receivedFrom}
                received
              />
            </div>
          </div>}

          {contact.sharedWith?.length > 0 &&
          <div className='flex-row'>
            <Description>Shared With</Description>
            <div className='contact-entry'>
              {contact.sharedWith.map(user =>
              <span className='chip-span'>
                <UserChip
                  contactId={contact.id}
                  user={user}
                />
              </span>)}
            </div>
          </div>}
        </div>
      </div>
      
      {sharing &&
      <div className='users-area'>
        <UsersList
          users={filteredUsers}
          setSelectedUserId={setSelectedUserId}
          scrollAreaHeight={scrollAreaHeight}
          scrollBarWidth={scrollBarWidth}
        />
      </div>}
    </div>
  );
};

export default ContactView;