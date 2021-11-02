import './App.css';
import { useDispatch } from 'react-redux';
import AppRouter from './components/AppRouter/AppRouter';
import { checkLoginStatus } from './state/actions/userThunk';
import { useEffect } from 'react';

const App = () => {
  const dispatch = useDispatch();

  useEffect(() => {
    dispatch(checkLoginStatus());
  }, [])

  return (
    <AppRouter />
  );
}

export default App;
