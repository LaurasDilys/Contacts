import './Contacts.css';
import { Divider, Input, InputAdornment, List, ListItem, ListItemText, TextField } from '@mui/material';
import SearchIcon from '@mui/icons-material/Search';
import { useEffect, useRef, useState } from 'react';
import ContactArea from './ContactArea';
import { useSelector } from 'react-redux';
import { contactsState } from '../../state/selectors';

//
//
//
// const mockContacts = [
//   {
//     id: 1,
//     firstName: 'Arnas',
//     lastName: 'B.',
//     phoneNumber: '37000000000',
//     email: null,
//     notes: null,
//     selected: false // first contact MUST BE selected
//     // if there are no created contacts, then
//     // the first and only contact will be "ME"
//   },
//   {
//     id: 2,
//     firstName: 'Benas',
//     lastName: 'C.',
//     phoneNumber: null,
//     email: null,
//     notes: null,
//     selected: false
//   },
//   {
//     id: 3,
//     firstName: 'Denas',
//     lastName: 'F.',
//     phoneNumber: null,
//     email: null,
//     notes: null,
//     selected: false
//   }
// ]

// let currentId = 3;
// for (let i = 0; i < 3; i++) {
//   const more = [];
//   mockContacts.forEach(c => {
//     more.push({...c});
//   });
//   more.forEach(c => c.id = ++currentId);
//   mockContacts.push(...more);
// }

// mockContacts.unshift({
//   id: 0,
//   firstName: 'Lauras',
//   lastName: 'Dilys',
//   phoneNumber: '37061417706',
//   alternativePhoneNumber: '37061536904',
//   email: 'lauras.dilys@gmail.com',
//   alternativeEmail: 'spotas@gmail.com',
//   dateOfBirth: '1988-10-17',
//   notes: 'This is my contact',
//   // ". Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
//   selected: false
// })
//
//
//

const searchFieldStyle = {
  width: 300,
  marginTop: 1,
};

const contactsListStyle = {
  width: 300,
  bgcolor: 'background.paper',
};

const getFullName = (firstName, lastName) => {
  let fullName = '';
  firstName?.length > 0 && (fullName += firstName);
  lastName?.length > 0 && (fullName += ` ${lastName}`);
  return fullName;
}

const generateNewContact = () => {
  return {
    firstName: null,
    lastName: null,
    phoneNumber: null,
    alternativePhoneNumber: null,
    email: null,
    alternativeEmail: null,
    dateOfBirth: null,
    notes: null,
  }
}

const sorted = contacts => {
  contacts.sort((a, b) =>
  a.firstName.localeCompare(b.firstName) || // first sorts by firstName
  a.lastName.localeCompare(b.lastName)); // then by lastName
  return contacts;
}

const Contacts = () => {
  const { contacts: allContacts } = useSelector(contactsState);
  const [creating, setCreating] = useState(false);

  const [contacts, setContacts] = useState(() => {
    if (allContacts.length > 0) allContacts[0].selected = true;
    return sorted(allContacts);
  });
  const [filteredContacts, setFilteredContacts] = useState(contacts);
  const [search, setSearch] = useState();
  const [contactsListHeight, setContactsListHeight] = useState();
  const contactsListRef = useRef();

  useEffect(() => { // when contactsState is updated (create / update / delete)
    const updatedState = sorted(allContacts);
    if (updatedState.length > 0 && updatedState.length < contacts.length) {
    // if a contact has been deleted, but there still are contacts left
      updatedState[0].selected = true;
    }
    setContacts(updatedState);
    setFilteredContacts(updatedState);
  }, [allContacts])

  const setCLH = () => {
    setContactsListHeight(window.innerHeight
      - contactsListRef.current.offsetTop - 17);
  };

  useEffect(() => {
    setCLH();
    const handleResize = () => {
      setCLH();
    }
    window.addEventListener('resize', handleResize);
    return _ => {
      window.removeEventListener('resize', handleResize);
  }});

  const handleSelect = id => {
    setCreating(false);
    const newState = contacts.map(c => {
      if (c.id === id) c.selected = true;
      else c.selected = false;
      return c;
    })
    setContacts(newState);
  };

  const searchResult = value => {
    setSearch(value);
    const newState = contacts.filter(c =>
      c.firstName?.toLocaleLowerCase().includes(value) ||
      c.lastName?.toLocaleLowerCase().includes(value));
    if (!newState.some(c => c.selected === true)) {
      contacts.forEach(c => c.selected = false);
      if (newState.length > 0) newState[0].selected = true;
    }
    setFilteredContacts(newState);
  }

  const handleSearch = ({ target }) => {
    setCreating(false);
    const value = target.value;
    searchResult(value);
  };

  useEffect(() => {
    (search === undefined && contacts.length > 0) &&
    (contacts[0].selected = true);
  }, [search])

  const handleNew = () => {
    searchResult('');
    contacts.forEach(c => c.selected = false);
    setCreating(true);
  }

  const handleSaveNew = () => {
    setCreating(false);
  }

  const handleCancelNew = () => {
    searchResult('');
    setCreating(false);
  }

  return (
    <div className='flex-row'>
      
      <div>
        <Input
          sx={searchFieldStyle}
          value={search}
          onChange={handleSearch}
          placeholder='Search Contacts'
          startAdornment={
            <InputAdornment position='start' style={{marginLeft: 12}}>
              <SearchIcon />
            </InputAdornment>}
        />
        <List
          sx={contactsListStyle}
          style={{ height: contactsListHeight, overflowY: 'auto' }}
          ref={contactsListRef}
        >
          {filteredContacts.map(c =>
          <div key={c.id}>
            <ListItem selected={c.selected} onClick={() => handleSelect(c.id)}>
              <ListItemText primary={getFullName(c.firstName, c.lastName)} />
            </ListItem>
            <Divider />
          </div>)}
        </List>
      </div>

      <div className='flex-grow-1'>
        <ContactArea
          handleNew={handleNew}
          handleSaveNew={handleSaveNew}
          handleCancelNew={handleCancelNew}
          contact={creating ? generateNewContact() : contacts.find(c => c.selected)}
        />
      </div>

    </div>
  );
};

export default Contacts;