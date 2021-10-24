import { Divider, Input, InputAdornment, List, ListItem, ListItemText } from "@mui/material";
import PersonSearchIcon from '@mui/icons-material/PersonSearch';
import { useEffect, useState } from "react";

const getFullName = (firstName, lastName) => {
  let fullName = '';
  firstName?.length > 0 && (fullName += firstName);
  lastName?.length > 0 && (fullName += ` ${lastName}`);
  return fullName;
}

const sorted = contacts => {
  contacts.sort((a, b) =>
  a.firstName.localeCompare(b.firstName) || // first sorts by firstName
  a.lastName.localeCompare(b.lastName) || // then by lastName
  a.userName.localeCompare(b.lastName)) // then by userName
  return contacts;
}

const searchFieldStyle = {
  width: 300,
  marginTop: 1,
}

const usersListStyle = {
  width: 300,
  bgcolor: 'background.paper',
}

const UsersList = ({ users, setSelectedUserId, scrollAreaHeight, scrollBarWidth }) => {
  //
  const allContacts = users;
  //
  const [contacts, setContacts] = useState([]);
  const [filteredContacts, setFilteredContacts] = useState([]);
  const [search, setSearch] = useState();

  useEffect(() => { // when contactsState is updated: initial render / create / update / delete
    const updatedState = sorted(allContacts);
    const selected = allContacts.find(c => c.selected);
    //
    console.log(selected === undefined ? null : selected.id)
    //
    setSelectedUserId(selected === undefined ? null : selected.id);
    if (updatedState.length > 0 && selected === undefined) {
      // if there are contacts and none are selected
      // select first
      updatedState[0].selected = true;
      setSelectedUserId(updatedState[0].id);
      //
      console.log(updatedState[0].id)
    }
    setContacts(updatedState);
    setFilteredContacts(updatedState);
  }, [allContacts])

  const handleSelect = id => {
    const newState = contacts.map(c => {
      if (c.id === id) c.selected = true;
      else c.selected = false;
      return c;
    })
    setContacts(newState);
    setSelectedUserId(id);
  };

  const searchResult = value => {
    setSearch(value);
    const newState = contacts.filter(c =>
      c.firstName?.toLocaleLowerCase().includes(value) ||
      c.lastName?.toLocaleLowerCase().includes(value) ||
      c.userName?.toLocaleLowerCase().includes(value));
    if (!newState.some(c => c.selected)) {
      contacts.forEach(c => c.selected = false);
      if (newState.length > 0) {
        newState[0].selected = true;
        setSelectedUserId(newState[0].id);
      } else setSelectedUserId(null);
    }
    setFilteredContacts(newState);
  }

  const handleSearch = ({ target }) => {
    const value = target.value;
    searchResult(value);
  };

  return (
    <>
      <Input
        sx={searchFieldStyle}
        value={search}
        onChange={handleSearch}
        placeholder='Search Users'
        startAdornment={
          <InputAdornment position='start' style={{marginLeft: 12}}>
            <PersonSearchIcon />
          </InputAdornment>}
      />
      <List
        sx={usersListStyle}
        style={{ height: scrollAreaHeight - scrollBarWidth + 2, overflowY: 'auto' }}
      >
        {filteredContacts.map(u =>
        <div key={u.id}>
          <ListItem selected={u.selected} onClick={() => handleSelect(u.id)}>
                                        {/* filteredUsers */}
            <ListItemText primary={getFullName(u.firstName, u.lastName)} secondary={u.userName} />
          </ListItem>
          <Divider />
        </div>)}
      </List>
    </>
  );
}

export default UsersList;