import { ListItem, ListItemText } from '@mui/material';
import { useSelector } from 'react-redux';
import { onConfirm } from '../ConfirmAlert/ConfirmAlert';
import ContactsMenu from '../ContactsProviders/ContactsMenu';
import { history } from './AppRouter';
import { contactsState } from '../../state/selectors';
import { OTHER } from '../../domain/contactTypes';

const TopNavItem = ({ title, path }) => {
  const { contacts: allContacts, selectedContacts } = useSelector(contactsState);

  const getTitle = () => {
    let result;
    if (path !== '/contacts' || !allContacts.some(c => c.type != OTHER)) {
      result = title;
    } else {
      result = selectedContacts[0];
      result += selectedContacts.slice(1).toLowerCase() + ' ';
      result += title;
    }
    return result;
  }

  const navigateToPath = () => history.push(path)
  
  const handleNavigation = () => {
    if (path !== '/logout') {
      navigateToPath();
    } else {
      onConfirm('log out', navigateToPath);
    }
  }

  return (
    <>
      <ListItem
        button
        onClick={handleNavigation}
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