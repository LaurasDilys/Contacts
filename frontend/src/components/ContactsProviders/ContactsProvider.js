import { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { contactsState } from '../../state/selectors';
import Contacts from '../Contacts/Contacts';
import { ALL, UNACCEPTED } from '../../domain/contactTypes';
import { setSelectedContactsAction } from '../../state/actions/contactsActions';

const sorted = contacts => {
  contacts.sort((a, b) =>
  a.firstName.localeCompare(b.firstName) || // first sort by firstName
  // then (if both a and b have last names) // then sort by last name
  b.lastName && a.lastName?.localeCompare(b.lastName));
  return contacts;
}

const ContactsProvider = () => {
  const { contacts: allContacts, selectedContacts } = useSelector(contactsState);
  const [previousContacts, setPreviousContacts] = useState(allContacts);
  const [filteredContacts, setFilteredContacts] = useState([]);
  const dispatch = useDispatch();

  const getContactsArray = () => {
    if (selectedContacts === ALL)
      return sorted(allContacts.filter(c => c.type !== UNACCEPTED));
      return sorted(allContacts.filter(c => c.type === selectedContacts));
  }

  const provideContacts = () => {
    const arrayWithCorrectSelectedContact = getContactsArray();
    if (arrayWithCorrectSelectedContact.length > 0 &&
      !arrayWithCorrectSelectedContact.some(c => c.selected)) {
      allContacts.forEach(c => c.selected = false);
      arrayWithCorrectSelectedContact[0].selected = true;
    }
    setFilteredContacts(arrayWithCorrectSelectedContact);
  }

  useEffect(() => {
    if (previousContacts.length === 0) {
      // contacts have just been retrieved from the backend
      provideContacts();
    } else if (getContactsArray().length === 0 && allContacts.length > 0 ||
      // there are no more contacts in the current selected section
      previousContacts.length < allContacts.length && selectedContacts !== ALL) {
      // or new contact has been created and user must be redirected to "All Contacts" to view it
      dispatch(setSelectedContactsAction(ALL));
    } else {
      provideContacts();
    }
    setPreviousContacts(allContacts);
  }, [allContacts])

  useEffect(() => {
    provideContacts();
  }, [selectedContacts])

  return (
    <Contacts providedContacts={filteredContacts} />
  );
}

export default ContactsProvider;