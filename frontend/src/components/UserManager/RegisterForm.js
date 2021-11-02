import { useState } from 'react';
import { Button, CssBaseline, Container, Grid, Typography, Tooltip } from '@mui/material';
import { Link } from 'react-router-dom';
import { makeStyles } from '@mui/styles';
import { useDispatch } from 'react-redux';
import ValidatedTextField from '../ValidatedTextField/ValidatedTextField';
import { register } from '../../state/actions/userThunk';

const useStyles = makeStyles({
  paper: {
    marginTop: 64,
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
  },
  avatar: {
    margin: 8,
  },
  form: {
    width: '100%',
    marginTop: 24,
  },
  submit: {
    marginTop: 24,
    marginBottom: 16,
  },
  link: {
    textDecoration: 'none',
    color: '#1565c0',
    '&:hover': {
      color: '#1e88e5'
    },
   '&:active': {
      color: '#1565c0'
    }
  }
});

const InitialFormData = {
  username: '',
  firstName: '',
  lastName: '',
  password: '',
  confirmPassword: ''
};

const RegisterForm = () => {
  const classes = useStyles();
  const [formData, setFormData] = useState(InitialFormData);
  const [isUsernameValid, setIsUsernameValid] = useState(false);
  const [isFirstNameValid, setIsFirstNameValid] = useState(false);
  const [isLastNameValid, setIsLastNameValid] = useState(false);
  const [isPasswordValid, setIsPasswordValid] = useState(false);
  const [isConfirmPasswordValid, setIsConfirmPasswordValid] = useState(false);

  const isFormValid =
    isUsernameValid &&
    isFirstNameValid &&
    isLastNameValid &&
    isPasswordValid &&
    isConfirmPasswordValid

  const dispatch = useDispatch();

  const onFormSubmit = (e) => {
    e.preventDefault();
    dispatch(register(formData));
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
          Register
        </Typography>
        <form className={classes.form} noValidate onSubmit={onFormSubmit}>
          <Grid container spacing={2}>
            <Grid item xs={12}>
              <ValidatedTextField
                autoComplete="username"
                autoFocus
                fullWidth
                id="username"
                label="Username"
                name="username"
                onChange={(e) => onFieldChange(e, 'username')}
                validationProps={{
                  isValid: isUsernameValid,
                  setIsValid: setIsUsernameValid,
                }}
                value={formData.username}
                variant="outlined"
              />
            </Grid>
            <Grid item sm={6} xs={12}>
              <ValidatedTextField
                autoComplete="firstname"
                fullWidth
                id="firstname"
                label="First name"
                name="firstname"
                onChange={(e) => onFieldChange(e, 'firstName')}
                validationProps={{
                  isValid: isFirstNameValid,
                  setIsValid: setIsFirstNameValid,
                }}
                value={formData.firstName}
                variant="outlined"
              />
            </Grid>
            <Grid item sm={6} xs={12}>
              <ValidatedTextField
                autoComplete="lastname"
                fullWidth
                id="lastname"
                label="Last name"
                name="lastname"
                onChange={(e) => onFieldChange(e, 'lastName')}
                validationProps={{
                  isValid: isLastNameValid,
                  setIsValid: setIsLastNameValid,
                }}
                value={formData.lastName}
                variant="outlined"
              />
            </Grid>
            <Grid item xs={12}>
              <Tooltip title={isPasswordValid  || formData.password === "" ?
                "" : "Password must contain at least six symbols, of which there must be at least one uppercase letter and at least one number"}
              >
                <ValidatedTextField
                  autoComplete="new-password"
                  fullWidth
                  id="password"
                  label="Password"
                  name="password"
                  onChange={(e) => onFieldChange(e, 'password')}
                  type="password"
                  validationProps={{
                    isValid: isPasswordValid,
                    setIsValid: setIsPasswordValid,
                    regexString: '^(.{0,5}|[^0-9]*|[^A-Z]*)$',
                    regexRuleReverse: true,
                    additionalCheck: (input) => {
                      setIsConfirmPasswordValid(input === formData.confirmPassword);
                      return true;
                    },
                  }}
                  value={formData.password}
                  variant="outlined"
                />
              </Tooltip>
            </Grid>
            <Grid item xs={12}>
              <Tooltip title={isConfirmPasswordValid || formData.confirmPassword === "" ?
                "" : "Passwords must match"}
              >
                <ValidatedTextField
                  autoComplete="new-password"
                  fullWidth
                  id="confirmPassword"
                  label="Confirm password"
                  name="confirmPassword"
                  onChange={(e) => onFieldChange(e, 'confirmPassword')}
                  type="password"
                  validationProps={{
                    isValid: isConfirmPasswordValid,
                    setIsValid: setIsConfirmPasswordValid,
                    regexString: '^(.{0,5}|[^0-9]*|[^A-Z]*)$',
                    regexRuleReverse: true,
                    additionalCheck: (input) => {
                      return input === formData.password;
                    },
                  }}
                  value={formData.confirmPassword}
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
            type="submit"
            variant="contained"
          >
            Register
          </Button>
          <Grid container justifyContent="flex-end">
            <Link to="/login" className={classes.link}>
              Already have an account? Login!
            </Link>
          </Grid>
        </form>
      </div>
    </Container>
  );
};

export default RegisterForm;