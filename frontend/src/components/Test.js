import axios from 'axios';
import { useState } from 'react';
import Button from '@mui/material/Button';

const host = 'https://localhost:44363/api/';
const timeout = 3000;

const Api = axios.create({
  baseURL: host,
  timeout: timeout,
  withCredentials: true
});

function Test() {
  const [state, setState] = useState('');

  const onLogin = () => {
    Api.post('login', {
      userName: 'lauras',
      password: 'lauras',
      remember: false
    }).then(
      (res) => {
        setState(res.data.userName);
      },
      (error) => {
        setState(error.response?.data);
      }
    );
  }

  const onTest = () => {
    Api.post('test').then(
      (res) => {
        setState(res.data === true ?
        'true' : 'false');
      },
      (error) => {
        setState(error.message);
      }
    );
  }

  const onLogOut = () => {
    Api.post('logout').then(
      (res) => {
        setState(res.data);
      });
  };

  return (
    <div className="App">
      <Button onClick={onLogin}>Login</Button>
      <Button onClick={onTest}>Get DateTime (Auth)</Button>
      <Button onClick={onLogOut}>Logout</Button>
      <h1>{state}</h1>
    </div>
  );
}

export default Test;
