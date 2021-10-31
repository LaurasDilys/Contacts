import { useState } from 'react';
import { makeStyles } from '@mui/styles';
import { useHistory } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';

import ValidatedTextField from './ValidatedTextField/ValidatedTextField';
import { Button, Container, CssBaseline, Grid, MenuItem, TextField, Typography } from '@mui/material';

const useStyles = makeStyles((theme) => ({
  paper: {
    // marginTop: theme.spacing(8),
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
  },
  avatar: {
    // margin: theme.spacing(1),
    // backgroundColor: theme.palette.secondary.main,
  },
  form: {
    width: '100%',
    // marginTop: theme.spacing(3),
  },
  submit: {
    // margin: theme.spacing(3, 0, 2),
  },
}));

const InitialFormData = {
  oldPassword: '',
  newPassword: '',
  newRepeatPassword: ''
};

export const doPasswordsMatch = (otherPassword, setIsOtherValid) => {
  return (input) => {
    const isEqual = input === otherPassword;
    setIsOtherValid(isEqual);
    return isEqual;
  };
};

const Test = () => {
  const classes = useStyles();
  const dispatch = useDispatch();
  const [formData, setFormData] = useState({ ...InitialFormData });
  const [isOldPasswordValid, setIsOldPasswordValid] = useState(false);
  const [isNewPasswordValid, setIsNewPasswordValid] = useState(false);
  const [isNewRepeatPasswordValid, setIsNewRepeatPasswordValid] = useState(false);

  const history = useHistory();

  const onSubmitClick = (e) => {
    e.preventDefault();
    // dispatch(SetNotificationAction({ isOpen: true, message: 'Information updated successfully.', type: 'success' }));
    history.push('/calendar');
  };

  // const saveButtonEnabled =
  //   (_.isEmpty(formData.oldPassword) &&
  //     _.isEmpty(formData.newRepeatPassword) &&
  //     _.isEmpty(formData.newPassword) ||
  //   (isOldPasswordValid && isNewPasswordValid && isNewRepeatPasswordValid);

  const onFieldChange = (e, field) => {
    formData[field.toString()] = e.target.value;
    setFormData({ ...formData });
  };

  return (
    <Container component="main" maxWidth="xs">
      <CssBaseline />
      <div className={classes.paper}>
        <Typography component="h1" variant="h5">
          Change user preferences
        </Typography>
        <form className={classes.form} noValidate>
          <Grid container spacing={2}>
            <Grid item xs={12}>
              <ValidatedTextField
                autoComplete="new-password"
                fullWidth
                id="oldPassword"
                label="Old password"
                name="oldPassword"
                onChange={(e) => onFieldChange(e, 'oldPassword')}
                type="password"
                validationProps={{
                  isValid: isOldPasswordValid,
                  setIsValid: setIsOldPasswordValid,
                }}
                value={formData.oldPassword}
                variant="outlined"
              />
            </Grid>
            <Grid item xs={12}>
              <ValidatedTextField
                autoComplete="new-password"
                fullWidth
                // helperText={validationMessages.passwordRule}
                id="newPassword"
                label="New password"
                name="newPassword"
                onChange={(e) => onFieldChange(e, 'newPassword')}
                type="password"
                validationProps={{
                  isValid: isNewPasswordValid,
                  setIsValid: setIsNewPasswordValid,
                  additionalCheck: (input) => {
                    setIsNewRepeatPasswordValid(input === formData.newRepeatPassword);
                    return true;
                  },
                  regexString: '^(.{0,5}|[^0-9]*|[^A-Z]*)$',
                  regexRuleReverse: true,
                }}
                value={formData.newPassword}
                variant="outlined"
              />
            </Grid>
            <Grid item xs={12}>
              <ValidatedTextField
                autoComplete="new-password"
                fullWidth
                // helperText={validationMessages.passwordMatchRule}
                id="repeatPassword"
                label="Repeat new password"
                name="repeatPassword"
                onChange={(e) => onFieldChange(e, 'newRepeatPassword')}
                type="password"
                validationProps={{
                  isValid: isNewRepeatPasswordValid,
                  setIsValid: setIsNewRepeatPasswordValid,
                  additionalCheck: (input) => {
                    return input === formData.newPassword;
                  },
                  regexString: '^(.{0,5}|[^0-9]*|[^A-Z]*)$',
                  regexRuleReverse: true,
                }}
                value={formData.newRepeatPassword}
                variant="outlined"
              />
            </Grid>
          </Grid>
          <Button
            className={classes.submit}
            color="primary"
            // disabled={!saveButtonEnabled}
            fullWidth
            onClick={onSubmitClick}
            type="submit"
            variant="contained"
          >
            Save changes
          </Button>
        </form>
      </div>
    </Container>
  );
}

export default Test;
