import axios from 'axios';

const host = 'https://localhost:44363/api/';
const timeout = 3000;

const Api = axios.create({
  baseURL: host,
  timeout: timeout,
  withCredentials: true
});

export default Api;