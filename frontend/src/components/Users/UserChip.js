import { Chip } from '@mui/material';
import { styled } from '@mui/system';
import { useDispatch } from 'react-redux';
import { stopSharingContact } from '../../state/actions/contactsThunk';
import { getFullName } from '../Contacts/Contacts';
import { stringToColor } from '../Contacts/ContactView';

const UserChip = ({ contactId, user, received }) => {
  const name = getFullName(user.firstName, user.lastName);
  const dispatch = useDispatch();

  const handleStopSharing = () => {
    dispatch(stopSharingContact(contactId, user.id));
  }

  return (
    received ?
    <StyledChip
      name={name}
      label={name}
    /> :
    <StyledChip
      name={name}
      label={name}
      onDelete={handleStopSharing}
    />
  );
}

const StyledChip = styled(Chip)(({ name }) => ({
  '&.MuiChip-root': {
    backgroundColor: stringToColor(name),
    color: 'ghostwhite',
  },
  '& .MuiChip-deleteIcon:hover': {
    color: 'ghostwhite'
  },
}));

export default UserChip;