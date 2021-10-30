import { Button } from '@mui/material';
import { confirmAlert } from 'react-confirm-alert';
import 'react-confirm-alert/src/react-confirm-alert.css';
import './ConfirmAlert.css';

export const onConfirm = (message, callbackFunction, variant) => {
  confirmAlert({
    customUI: ({ onClose }) => {

      const text = `Are you sure you want to ${message}?`

      return (
        <div className='alert-box'>
          <h1>Confirm to continue</h1>
          {variant === 'fullText' ?
          message :
          <p>Are you sure you want to {message}?</p>}
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