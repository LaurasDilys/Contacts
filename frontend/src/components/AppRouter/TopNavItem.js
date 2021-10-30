import { ListItem, ListItemText } from '@mui/material';
import { useSelector } from 'react-redux';
import ContactsMenu from '../ContactsProviders/ContactsMenu';
import { contactsState } from '../../state/selectors';
import ProfileMenu from '../Profile/ProfileMenu';
import { Link } from 'react-router-dom';

const TopNavItem = ({ title, path }) => {
  const { contacts: allContacts, selectedContacts } = useSelector(contactsState);

  const getTitle = () => {
    let result;
    if (path !== '/contacts') {
      result = title;
    } else {
      result = selectedContacts[0];
      result += selectedContacts.slice(1).toLowerCase() + ' ';
      result += title;
    }
    return result;
  }

  return (
    path === '/profile' ?
    <ProfileMenu /> :
    <>
      <ListItem
        button
        component={Link}
        to={path}
        style={{ width: 'fit-content' }}
      >
        <ListItemText primary={getTitle()} />
      </ListItem>
      {path === '/contacts' &&
      <ContactsMenu />}
    </>
  );
}

export default TopNavItem;