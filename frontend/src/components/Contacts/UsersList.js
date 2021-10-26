import { Divider, Input, InputAdornment, List, ListItem, ListItemText } from "@mui/material";
import PersonSearchIcon from '@mui/icons-material/PersonSearch';
import { useEffect, useState } from "react";

const getFullName = (firstName, lastName) => {
  let fullName = '';
  firstName?.length > 0 && (fullName += firstName);
  lastName?.length > 0 && (fullName += ` ${lastName}`);
  return fullName;
}

const sorted = users => {
  users.sort((a, b) =>
  a.firstName.localeCompare(b.firstName) || // first sorts by firstName
  a.lastName.localeCompare(b.lastName) || // then by lastName
  a.userName.localeCompare(b.lastName)) // then by userName
  return users;
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
  // const users = usersFromProps;
  //
  const [allUsers, setAllUsers] = useState([]);
  const [filteredUsers, setFilteredUsers] = useState([]);
  const [search, setSearch] = useState();

  useEffect(() => { // initial render / users change
    const updatedState = sorted(users);
    const selected = users.find(c => c.selected);
    setSelectedUserId(selected === undefined ? null : selected.id);
    if (updatedState.length > 0 && selected === undefined) {
      // if there are users and none are selected
      // select first
      updatedState[0].selected = true;
      setSelectedUserId(updatedState[0].id);
    }
    setAllUsers(updatedState);
    setFilteredUsers(updatedState);
  }, [users])

  const handleSelect = id => {
    const newState = allUsers.map(c => {
      if (c.id === id) c.selected = true;
      else c.selected = false;
      return c;
    })
    setAllUsers(newState);
    setSelectedUserId(id);
  };

  const searchResult = value => {
    setSearch(value);
    const newState = allUsers.filter(c =>
      c.firstName?.toLocaleLowerCase().includes(value) ||
      c.lastName?.toLocaleLowerCase().includes(value) ||
      c.userName?.toLocaleLowerCase().includes(value));
    if (!newState.some(c => c.selected)) {
      users.forEach(c => c.selected = false);
      if (newState.length > 0) {
        newState[0].selected = true;
        setSelectedUserId(newState[0].id);
      } else setSelectedUserId(null);
    }
    setFilteredUsers(newState);
  }

  const handleSearch = ({ target }) => {
    const value = target.value;
    searchResult(value);
  };

  return (
    users.length > 0 ?
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
        {filteredUsers.map(u =>
        <div key={u.id}>
          <ListItem selected={u.selected} onClick={() => handleSelect(u.id)}>
            <ListItemText primary={getFullName(u.firstName, u.lastName)} secondary={u.userName} />
          </ListItem>
          <Divider />
        </div>)}
      </List>
    </> :
    <>
      <Divider style={{marginTop: 39}} />
      <h1 style={{
        color: '#bdbdbd',
        margin: '79px 50px',
        whiteSpace: 'nowrap'
      }}>
        No users currently available
      </h1>
    </>
  );
}

export default UsersList;