import { useState } from 'react';
import { makeStyles } from '@mui/styles';
import { useDispatch, useSelector } from 'react-redux';
import ValidatedTextField from '../ValidatedTextField/ValidatedTextField';
import { Button, Container, CssBaseline, Grid, Tooltip, Typography } from '@mui/material';
import { changePassword } from '../../state/actions/userThunk';
import { userState } from '../../state/selectors';

const useStyles = makeStyles(() => ({
  paper: {
    marginTop: 64,
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center'
  },
  avatar: {
    margin: 8
  },
  form: {
    width: '100%',
    maxWidth: '330px',
    marginTop: 24
  },
  submit: {
    marginTop: 24,
    marginBottom: 16
  },
}));

const InitialFormData = {
  currentPassword: '',
  newPassword: '',
  confirmNewPassword: ''
};

export const doPasswordsMatch = (otherPassword, setIsOtherValid) => {
  return (input) => {
    const isEqual = input === otherPassword;
    setIsOtherValid(isEqual);
    return isEqual;
  };
};

const ChangePassword = () => {
  const classes = useStyles();
  const [formData, setFormData] = useState({ ...InitialFormData });
  const [isCurrentPasswordValid, setIsCurrentPasswordValid] = useState(false);
  const [isNewPasswordValid, setIsNewPasswordValid] = useState(false);
  const [isConfirmNewPasswordValid, setIsConfirmNewPasswordValid] = useState(false);
  const { user } = useSelector(userState);
  const dispatch = useDispatch();

  const isFormValid =
    isCurrentPasswordValid &&
    isNewPasswordValid &&
    isConfirmNewPasswordValid

  const onSubmitClick = (e) => {
    e.preventDefault();
    
    setIsCurrentPasswordValid(false);
    setIsNewPasswordValid(false);
    setIsConfirmNewPasswordValid(false);
    setFormData({ ...InitialFormData });

    const { currentPassword, newPassword } = formData;
    dispatch(changePassword(user.id, {
      currentPassword: currentPassword,
      newPassword: newPassword
    }))
  };

  const onFieldChange = (e, field) => {
    formData[field.toString()] = e.target.value;
    setFormData({ ...formData });
  };

  return (
    <Container component="main" maxWidth="xs">
      <CssBaseline />
      <div className={classes.paper}>
        <Typography component="h1" variant="h5">
          Change password
        </Typography>
        <form className={classes.form} noValidate>
          <Grid container spacing={2}>
            <Grid item xs={12}>
              <ValidatedTextField
                autoComplete="new-password"
                fullWidth
                id="currentPassword"
                label="Current Password"
                name="currentPassword"
                onChange={(e) => onFieldChange(e, 'currentPassword')}
                type="password"
                validationProps={{
                  isValid: isCurrentPasswordValid,
                  setIsValid: setIsCurrentPasswordValid,
                }}
                value={formData.currentPassword}
                variant="outlined"
              />
            </Grid>
            <Grid item xs={12}>
              <Tooltip title={isNewPasswordValid  || formData.newPassword === "" ?
                "" : "Password must contain at least six symbols, of which there must be at least one uppercase letter and at least one number"}
              >
                <ValidatedTextField
                  autoComplete="new-password"
                  fullWidth
                  id="newPassword"
                  label="New Password"
                  name="newPassword"
                  onChange={(e) => onFieldChange(e, 'newPassword')}
                  type="password"
                  validationProps={{
                    isValid: isNewPasswordValid,
                    setIsValid: setIsNewPasswordValid,
                    additionalCheck: (input) => {
                      setIsConfirmNewPasswordValid(input === formData.confirmNewPassword);
                      return true;
                    },
                    regexString: '^(.{0,5}|[^0-9]*|[^A-Z]*)$',
                    regexRuleReverse: true,
                  }}
                  value={formData.newPassword}
                  variant="outlined"
                />
              </Tooltip>
            </Grid>
            <Grid item xs={12}>
              <Tooltip title={isConfirmNewPasswordValid || formData.confirmNewPassword === "" ?
                "" : "Passwords must match"}
              >
                <ValidatedTextField
                  autoComplete="new-password"
                  fullWidth
                  id="repeatPassword"
                  label="Confirm New Password"
                  name="repeatPassword"
                  onChange={(e) => onFieldChange(e, 'confirmNewPassword')}
                  type="password"
                  validationProps={{
                    isValid: isConfirmNewPasswordValid,
                    setIsValid: setIsConfirmNewPasswordValid,
                    additionalCheck: (input) => {
                      return input === formData.newPassword;
                    },
                    regexString: '^(.{0,5}|[^0-9]*|[^A-Z]*)$',
                    regexRuleReverse: true,
                  }}
                  value={formData.confirmNewPassword}
                  variant="outlined"
                />
              </Tooltip>
            </Grid>
          </Grid>
          <Button
            className={classes.submit}
            color="primary"
            disabled={!isFormValid}
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

export default ChangePassword;
