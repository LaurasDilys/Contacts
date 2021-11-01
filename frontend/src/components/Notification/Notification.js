import { makeStyles } from '@mui/styles';
import { Alert, Snackbar } from '@mui/material';
import { useDispatch, useSelector } from 'react-redux';
import { notificationState } from '../../state/selectors';
import { clearNotification } from '../../state/actions/notificationActions';

const useStyles = makeStyles(() => ({
  root: { top: 60 }
}));

const Notification = () => {
  const classes = useStyles();
  const { message, type, isOpen } = useSelector(notificationState);
  const dispatch = useDispatch();

  const handleClose = (event, reason) => {
    if (reason === 'clickaway') return;
    dispatch(clearNotification());
  };

  return (
    <Snackbar
      anchorOrigin={{ vertical: 'top', horizontal: 'right' }}
      autoHideDuration={3000}
      className={classes.root}
      onClose={handleClose}
      open={isOpen}
    >
      <Alert severity={type}>{message}</Alert>
    </Snackbar>
  );
}

export default Notification;
