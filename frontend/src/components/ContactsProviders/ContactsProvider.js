import { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { contactsState } from '../../state/selectors';
import Contacts from '../Contacts/Contacts';
import { ALL, UNACCEPTED } from '../../domain/contactTypes';

const sorted = contacts => {
  contacts.sort((a, b) =>
  a.firstName.localeCompare(b.firstName) || // first sort by firstName
  // then (if both a and b have last names) // then sort by last name
  b.lastName && a.lastName?.localeCompare(b.lastName));
  return contacts;
}

const ContactsProvider = () => {
  const { contacts: allContacts, selectedContacts } = useSelector(contactsState);
  const [PREVSTATE, setPREVSTATE] = useState(allContacts);
  const [filteredContacts, setFilteredContacts] = useState([]);
  const dispatch = useDispatch();

  const determine = () => {
    let filter = [];
    if (selectedContacts === ALL) {
      filter = sorted(allContacts.filter(c => c.type !== UNACCEPTED));
    } else {
      filter = sorted(allContacts.filter(c => c.type === selectedContacts));
    }
    return filter;
  }

  useEffect(() => {
    // compare with PREVSTATE
    // possibly dispatch setSelectedContacts
  }, [allContacts])

  useEffect(() => {
//
    const filtered = determine();
//
    if (filtered.length > 0 && !filtered.some(c => c.selected)) {
      allContacts.forEach(c => c.selected = false);
      filtered[0].selected = true;
    }
//
    setFilteredContacts(filtered);
//
  }, [selectedContacts, allContacts])

  return (
    <Contacts providedContacts={filteredContacts} />
  );
}

export default ContactsProvider;