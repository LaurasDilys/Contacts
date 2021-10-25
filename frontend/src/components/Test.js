import { Button } from '@mui/material';
import { onConfirm } from '../components/ConfirmAlert/ConfirmAlert'

const Test = () => {

  const handleConfirm = () => {
    alert('confirmed')
  }

  return (
    <div style={{display: 'flex', justifyContent: 'center', height: '100vh'}}>
      <Button onClick={() => onConfirm('stop sharing this contact', handleConfirm)}>Confirm dialog</Button>
    </div>
  );
}

export default Test;