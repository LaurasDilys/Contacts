import { useState } from 'react';
import { Checkbox, Button, Box, FormControlLabel, Typography } from '@mui/material';
import { makeStyles } from '@mui/styles';
import { Link } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { login } from '../../state/actions/authenticationThunk';
import './LoginForm.css';
import ValidatedTextField from '../ValidatedTextField/ValidatedTextField';

const useStyles = makeStyles({
  inputfield: {
    marginTop: '10px',
    marginBottom: '5px',
  },
  cboxtext: {
    fontSize: '0.8rem',
  },
  login: {
    marginRight: 5,
  },
  register: {
    marginLeft: 5,
  },
  header: {
    margin: 10,
    textAlign: 'center',
  },
});

const InitialFormData = {
  username: '',
  password: '',
  remember: false,
};

const LoginForm = () => {
  const classes = useStyles();

  const [formData, setFormData] = useState(InitialFormData);
  const [isUsernameValid, setIsUsernameValid] = useState(false);
  const [isPasswordValid, setIsPasswordValid] = useState(false);
  const dispatch = useDispatch();

  const isFormValid = isUsernameValid && isPasswordValid;

  const onFormSubmit = (e) => {
    e.preventDefault();
    const { username, password, remember } = formData;
    dispatch(
      login({
        userName: username,
        password: password,
        remember: remember,
      })
    );
  };

  const onUsernameChange = (e) => {
    setFormData({ ...formData, username: e.target.value });
  };

  const onPasswordChange = (e) => {
    setFormData({ ...formData, password: e.target.value });
  };

  const onRememberChange = (e) => {
    setFormData({ ...formData, remember: e.target.checked });
  };

  return (
    <form className="login-form" onSubmit={onFormSubmit}>
      <Box className={classes.header}>
        <Typography component="h1" variant="h5">
          Login
        </Typography>
      </Box>
      <Box>
        <ValidatedTextField
          autoFocus
          className={classes.inputfield}
          error={false}
          fullWidth
          id="username"
          label="Username"
          onChange={onUsernameChange}
          validationProps={{
            isValid: isUsernameValid,
            setIsValid: setIsUsernameValid,
            regexString: '[._]{2,}',
            regexRuleReverse: true,
          }}
          value={formData.username}
          variant="outlined"
        />
      </Box>
      <Box>
        <ValidatedTextField
          className={classes.inputfield}
          error={false}
          fullWidth
          id="password"
          label="Password"
          onChange={onPasswordChange}
          type="password"
          validationProps={{
            isValid: isPasswordValid,
            setIsValid: setIsPasswordValid,
          }}
          value={formData.password}
          variant="outlined"
        />
      </Box>
      <Box>
        <FormControlLabel
          control={<Checkbox color="primary" name="remember-user" onChange={onRememberChange} />}
          label={<Typography className={classes.cboxtext}>Remember me</Typography>}
        />
      </Box>
      <Box>
        <Button className={classes.login} color="primary" disabled={!isFormValid} type="submit" variant="contained">
          Sign in
        </Button>
        <Button className={classes.register} color="primary" component={Link} to="/register" variant="contained">
          Register
        </Button>
      </Box>
    </form>
  );
};
export default LoginForm;
