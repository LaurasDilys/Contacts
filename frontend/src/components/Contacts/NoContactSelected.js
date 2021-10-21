import { Button, Divider } from "@mui/material";
import AddIcon from '@mui/icons-material/Add';

const NoContactSelected = ({ handleNew }) => {
  return (
    <div>
      <div className='contact-area-top'>
        <Button onClick={handleNew}>
          <AddIcon sx={{ marginBottom: '2px' }} />
          <span className='button-span'>New</span>
        </Button>
        <Divider />
        <h1 style={{color: '#bdbdbd', marginLeft: 25}}>No Contact Selected</h1>
      </div>
    </div>
  );
}

export default NoContactSelected;