import { Button, Divider, FormControlLabel } from '@mui/material';
import { useEffect, useState } from 'react';
import { useSelector } from 'react-redux';
import { userState } from '../../state/selectors';
import Details from '../Contacts/Details';
// import './Profile.css';
import { styled } from '@mui/material/styles';
import Switch from '@mui/material/Switch';
import MyContactView from './MyContactView';
import MyContactEdit from './MyContactEdit';

const MyContact = () => {
  const { user } = useSelector(userState);
  const [editing, setEditing] = useState(false);
  const [scrollAreaHeight, setScrollAreaHeight] = useState();
  const [scrollBarWidth, setScrollBarWidth] = useState(0);

  const handleResize = () => {
    if (scrollBarWidth === 0 && window.innerWidth - document.documentElement.clientWidth > 0) {
      setScrollBarWidth(window.innerWidth - document.documentElement.clientWidth); // sets scrollBarWidth only once
    }
    const xScrollBar = document.body.scrollWidth > window.innerWidth ? scrollBarWidth : 0;
    setScrollAreaHeight(window.innerHeight - xScrollBar - 2);
  };

  useEffect(() => {
    window.addEventListener('resize', handleResize);
    return _ => {
      window.removeEventListener('resize', handleResize);
  }}, []);

  useEffect(() => {
    handleResize();
  });

  return (
    user === null ?
    null :
    <div className='flex-row center'>
      {editing ?
      <MyContactEdit setEditing={setEditing} scrollAreaHeight={scrollAreaHeight} /> :
      <MyContactView setEditing={setEditing} scrollAreaHeight={scrollAreaHeight} />}
    </div>
  );
}

// const Android12Switch = styled(Switch)(({ theme }) => ({
//   padding: 8,
//   '& .MuiSwitch-track': {
//     borderRadius: 22 / 2,
//     '&:before, &:after': {
//       content: '""',
//       position: 'absolute',
//       top: '50%',
//       transform: 'translateY(-50%)',
//       width: 16,
//       height: 16,
//     },
//     '&:before': {
//       backgroundImage: `url('data:image/svg+xml;utf8,<svg xmlns="http://www.w3.org/2000/svg" height="16" width="16" viewBox="0 0 24 24"><path fill="${encodeURIComponent(
//         theme.palette.getContrastText(theme.palette.primary.main),
//       )}" d="M21,7L9,19L3.5,13.5L4.91,12.09L9,16.17L19.59,5.59L21,7Z"/></svg>')`,
//       left: 12,
//     },
//     '&:after': {
//       backgroundImage: `url('data:image/svg+xml;utf8,<svg xmlns="http://www.w3.org/2000/svg" height="16" width="16" viewBox="0 0 24 24"><path fill="${encodeURIComponent(
//         theme.palette.getContrastText(theme.palette.primary.main),
//       )}" d="M19,13H5V11H19V13Z" /></svg>')`,
//       right: 12,
//     },
//   },
//   '& .MuiSwitch-thumb': {
//     boxShadow: 'none',
//     width: 16,
//     height: 16,
//     margin: 2,
//   },
// }));

// <FormControlLabel
//   control={
//     <Android12Switch
//       checked={showContact}
//       onChange={handleContactVisiblityChange}
//     />
//   }
//   label="Android 12"
// />

export default MyContact;