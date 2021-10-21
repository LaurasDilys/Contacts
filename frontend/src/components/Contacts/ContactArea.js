import { useEffect, useState } from "react";
import ContactEdit from "./ContactEdit";
import ContactView from "./ContactView";

const ContactArea = ({ contact }) => {
  const [editing, setEditing] = useState(false);

  useEffect(() => {
    setEditing(false);
  }, [contact])

  return (
    <div>
      {contact === undefined ?
      <h1>No Contact Selected</h1> :
      <div>
        {
          editing === true ?
          <ContactEdit contact={contact} setEditing={setEditing} /> :
          <ContactView contact={contact} setEditing={setEditing} />
        }
      </div>}
    </div>
  );
};

export default ContactArea;