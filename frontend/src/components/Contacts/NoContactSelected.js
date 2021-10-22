import { Button, Divider } from "@mui/material";
import AddIcon from '@mui/icons-material/Add';
import { useSelector } from "react-redux";
import { contactsState } from "../../state/selectors";

const NoContactSelected = ({ handleNew }) => {
  const { contacts: allContacts } = useSelector(contactsState);

  return (
    <div>
      <div className='contact-area-top'>
        <Button onClick={handleNew}>
          <AddIcon sx={{ marginBottom: '2px' }} />
          <span className='button-span'>New</span>
        </Button>
        <Divider />
        <h1 style={{color: '#bdbdbd', marginLeft: 25}}>
          {allContacts.length > 0 ? 'No Contact Selected' : 'No Contacts Available'}
        </h1>
      </div>
    </div>
  );
}

export default NoContactSelected;