import { Button } from '@mui/material';
import { confirmAlert } from 'react-confirm-alert';
import 'react-confirm-alert/src/react-confirm-alert.css';
import './ConfirmAlert.css';

export const onConfirm = (text, callbackFunction) => {
  confirmAlert({
    customUI: ({ onClose }) => {

      const fullText = `Are you sure you want to ${text}?`

      return (
        <div className='alert-box'>
          <h1>Confirm to continue</h1>
          <p>{fullText}</p>
          <div className='alert-buttons'>
            <Button
              variant='outlined'
              autoFocus
              onClick={() => {
                onClose();
                callbackFunction();
              }}
            >
              Yes
            </Button>
            <Button
              variant='outlined'
              onClick={onClose}
            >
              No
            </Button>
          </div>
        </div>
      );
    }
  });
} 