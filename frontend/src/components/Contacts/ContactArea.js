import { Button } from "@mui/material";
import { useEffect, useState } from "react";

const ContactArea = ({ contact }) => {
  const [edit, setEdit] = useState('');

  useEffect(() => {
    setEdit('');
  }, [contact])

  return (
    <div>
      {contact === undefined ?
      <h1>No Contact Selected</h1> :
      <div>
        {edit === '' && <Button onClick={() => setEdit('EDITING')}>Edit</Button>}
        <h1>{edit} {contact.firstName}</h1>
      </div>}
    </div>
  );
};

export default ContactArea;