import './App.css';
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

function App() {
  const [state, setState] = useState('');

  const onLogin = () => {
    Api.post('login', {
      username: 'logged in',
      password: '',
      remember: false
    }).then(
      (res) => {
        setState(res.data);
      },
      (error) => {
        setState(error.message);
      }
    );
  }

  const onTest = () => {
    Api.post('test').then(
      (res) => {
        setState(res.data);
      },
      (error) => {
        setState(error.message);
      }
    );
  }

  return (
    <div className="App">
      <Button onClick={onLogin}>Login</Button>
      <Button onClick={onTest}>Get DateTime (Auth)</Button>
      <h1>{state}</h1>
    </div>
  );
}

export default App;
