import { Chip } from '@mui/material';
import { styled } from '@mui/system';
import { getFullName } from '../Contacts/Contacts';
import { stringToColor } from '../Contacts/ContactView';

const UserChip = ({ contactId, user, received }) => {
  const name = getFullName(user.firstName, user.lastName);

  const stopSharing = () => {

  }

  return (
    received ?
    <StyledChip
      style={{ height: 60 }}
      name={name}
      label={
        <>
          <p>{name}</p>
          <p>{user.userName}</p>
        </>
      }
    /> :
    <StyledChip
      name={name}
      label={name}
      onDelete={stopSharing}
    />
  );
}

const StyledChip = styled(Chip)(({ name }) => ({
  '&.MuiChip-root': {
    backgroundColor: stringToColor(name),
    color: 'ghostwhite',
  },
  // '& .MuiChip-deleteIcon': {
  //   color: 'ghostwhite'
  // },
  '& .MuiChip-deleteIcon:hover': {
    color: 'ghostwhite'
  },
}));

export default UserChip;