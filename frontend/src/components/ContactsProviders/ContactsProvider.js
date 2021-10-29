import { useEffect, useState } from 'react';
import { useSelector } from 'react-redux';
import { contactsState } from '../../state/selectors';
import Contacts from '../Contacts/Contacts';
import { ALL, UNACCEPTED } from '../../domain/contactTypes';

const ContactsProvider = () => {
  const { contacts: allContacts, selectedContacts } = useSelector(contactsState);
  const [filteredContacts, setFilteredContacts] = useState([]);

  const filterProvidedContacts = () => {
    if (selectedContacts === ALL) {
      setFilteredContacts(allContacts.filter(c => c.type !== UNACCEPTED));
    } else {
      setFilteredContacts(allContacts.filter(c => c.type === selectedContacts));
    }
  }

  useEffect(() => {
    filterProvidedContacts();
  }, [allContacts])

  useEffect(() => {
    allContacts.forEach(c => c.selected = false);
    filterProvidedContacts();
  }, [selectedContacts])

  return (
    <Contacts providedContacts={filteredContacts} />
  );
}

export default ContactsProvider;