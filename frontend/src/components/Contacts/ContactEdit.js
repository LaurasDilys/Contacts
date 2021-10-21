import { Button } from "@mui/material";

const ContactEdit = ({ contact, setEditing }) => {

  return (
    <div>
      <Button onClick={() => setEditing(false)}>Cancel</Button>
      <h1>EDITING {contact.firstName}</h1>
    </div>
  );
};

export default ContactEdit;