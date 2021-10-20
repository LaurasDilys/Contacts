import './Contacts.css';
import { Divider, Input, InputAdornment, List, ListItem, ListItemText, TextField } from '@mui/material';
import SearchIcon from '@mui/icons-material/Search';
import { useEffect, useRef, useState } from 'react';
import ContactArea from './ContactArea';

//
//
//
const mockContacts = [
  {
    id: 1,
    firstName: 'Arnas',
    lastName: 'B.',
    selected: false // first contact MUST BE selected
    // if there are no created contacts, then
    // the first and only contact will be "ME"
  },
  {
    id: 2,
    firstName: 'Benas',
    lastName: 'C.',
    selected: false
  },
  {
    id: 3,
    firstName: 'Denas',
    lastName: 'F.',
    selected: false
  }
]

let currentId = 3;
for (let i = 0; i < 3; i++) {
  const more = [];
  mockContacts.forEach(c => {
    more.push({...c});
  });
  more.forEach(c => c.id = ++currentId);
  mockContacts.push(...more);
}
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

const Contacts = () => {
  const [contacts, setContacts] = useState(mockContacts);
  const [filteredContacts, setFilteredContacts] = useState(contacts);
  const [search, setSearch] = useState();
  const [contactsListHeight, setContactsListHeight] = useState();
  const contactsListRef = useRef();

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
    const newState = contacts.map(c => {
      if (c.id === id) c.selected = true;
      else c.selected = false;
      return c;
    })
    setContacts(newState);
  };

  const handleSearch = ({ target }) => {
    const value = target.value;
    setSearch(value);
    const newState = contacts.filter(c =>
      c.firstName.toLocaleLowerCase().includes(value) ||
      c.lastName.toLocaleLowerCase().includes(value));
    if (!newState.some(c => c.selected === true)) {
      contacts.forEach(c => c.selected = false);
      if (newState.length > 0) newState[0].selected = true;
    }
    setFilteredContacts(newState);
  };

  useEffect(() => {
    search === undefined && (contacts[0].selected = true);
  }, [search])

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
              <ListItemText primary={`${c.firstName} ${c.lastName}`} />
            </ListItem>
            <Divider />
          </div>)}
        </List>
      </div>

      <ContactArea contact={contacts.find(c => c.selected)} />

    </div>
  );
};

export default Contacts;