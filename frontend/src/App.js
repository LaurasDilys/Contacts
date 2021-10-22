import './App.css';
import { useDispatch } from 'react-redux';
import AppRouter from './components/AppRouter/AppRouter';
import PhoneNumberTest from './components/PhoneNumberTest';
import Test from './components/Test';
import { checkLoginStatus } from './state/actions/authenticationThunk';
import { useEffect } from 'react';

const App = () => {
  const dispatch = useDispatch();

  useEffect(() => {
    dispatch(checkLoginStatus());
  }, [])

  return (
    <AppRouter />
    // <Test />
    // <PhoneNumberTest />
  );
}

export default App;
