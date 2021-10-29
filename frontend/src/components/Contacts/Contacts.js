import './Contacts.css';
import { Divider, Input, InputAdornment, List, ListItem, ListItemText, TextField } from '@mui/material';
import SearchIcon from '@mui/icons-material/Search';
import { useEffect, useLayoutEffect, useRef, useState } from 'react';
import ContactArea from './ContactArea';
import { useSelector } from 'react-redux';
import { contactsState } from '../../state/selectors';
import useResizeObserver from '@react-hook/resize-observer'
import { ME } from '../../domain/contactTypes';

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

const useSize = (target) => {
  const [size, setSize] = useState()

  useLayoutEffect(() => {
    setSize(target.current.getBoundingClientRect())
  }, [target])

  useResizeObserver(target, (entry) => setSize(entry.contentRect))
  return size;
}

export const getFullName = (firstName, lastName) => {
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

const searchFieldStyle = {
  width: 300,
  marginTop: 1,
};

const contactsListStyle = {
  width: 300,
  bgcolor: 'background.paper',
};

const Contacts = ({ providedContacts }) => {
  const [creating, setCreating] = useState(false);
  const [contacts, setContacts] = useState([]);
  const [filteredContacts, setFilteredContacts] = useState([]);
  const [search, setSearch] = useState();
  const [scrollAreaHeight, setScrollAreaHeight] = useState();
  const [scrollBarWidth, setScrollBarWidth] = useState(0);
  const contactsListRef = useRef(null);
  const contactAreaDivRef = useRef(null);
  const contactAreaSize = useSize(contactAreaDivRef);

  useEffect(() => { // when providedContacts are updated
    setSearch('');
    setContacts(providedContacts);
    setFilteredContacts(providedContacts);
  }, [providedContacts])

  const handleResize = () => {
    if (scrollBarWidth === 0 && window.innerWidth - document.documentElement.clientWidth > 0) {
      setScrollBarWidth(window.innerWidth - document.documentElement.clientWidth); // sets scrollBarWidth only once
    }
    const xScrollBar = document.body.scrollWidth > window.innerWidth ? scrollBarWidth : 0;
    setScrollAreaHeight(window.innerHeight - contactsListRef.current.offsetTop
      - xScrollBar - 2);
  };

  useEffect(() => {
    window.addEventListener('resize', handleResize);
    return _ => {
      window.removeEventListener('resize', handleResize);
  }}, []);

  useEffect(() => {
    handleResize();
  });

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
    let newState;
    setSearch(value);
    if (value.toLocaleLowerCase() === 'me') {
      newState = contacts.filter(c =>
        c.firstName?.toLocaleLowerCase().includes(value) ||
        c.lastName?.toLocaleLowerCase().includes(value) ||
        c.me);
    } else {
      newState = contacts.filter(c =>
        c.firstName?.toLocaleLowerCase().includes(value) ||
        c.lastName?.toLocaleLowerCase().includes(value));
    }
    if (!newState.some(c => c.selected)) {
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

  // useEffect(() => {
  //   (search === undefined && contacts.length > 0) &&
  //   (contacts[0].selected = true);
  // }, [search])

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
          style={{ height: scrollAreaHeight - scrollBarWidth + 2, overflowY: 'auto' }}
          ref={contactsListRef}
        >
          {filteredContacts.map(c =>
          <div key={c.id}>
            <ListItem selected={c.selected} onClick={() => handleSelect(c.id)}>
              <ListItemText primary={
                c.me ?
                `${getFullName(c.firstName, c.lastName)} ${ME}` :
                getFullName(c.firstName, c.lastName)
              }/>
            </ListItem>
            <Divider />
          </div>)}
        </List>
      </div>

      <div className='flex-grow-1' ref={contactAreaDivRef}>

        {/* received.any()
              && snackbar: 'You have received a new contact, theat needs to be confirmed.'
                           'You have received new contacts, theat need to be confirmed.'
        
        {received ?
        //BTN: Remove this contact || Confirm All - Confirm this contact - Reject this contact
        <ReceivedContact              unconfirmed={unconfirmed} /> :
        //
        //
        //<ContactArea ...props />
        */}

        <ContactArea
          handleNew={handleNew}
          handleSaveNew={handleSaveNew}
          handleCancelNew={handleCancelNew}
          contact={creating ? generateNewContact() : contacts.find(c => c.selected)}
          scrollAreaHeight={scrollAreaHeight}
          scrollBarWidth={scrollBarWidth}
        />
      </div>

    </div>
  );
};

export default Contacts;