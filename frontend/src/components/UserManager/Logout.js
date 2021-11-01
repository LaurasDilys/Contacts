import { useEffect } from 'react';
import { useDispatch } from 'react-redux';
import { logout } from '../../state/actions/userThunk';

const Logout = () => {
  const dispatch = useDispatch();
  
  useEffect(() => {
    dispatch(logout());
  }, [])

  return null;
}

export default Logout;