import { useSelector } from 'react-redux';
import { contactsState } from '../../state/selectors';
import Contacts from '../Contacts/Contacts';

const ContactsProvider = () => {
  const { contacts: allContacts } = useSelector(contactsState);

  return (
    <Contacts providedContacts={allContacts} />
  );
}

export default ContactsProvider;