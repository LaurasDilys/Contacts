import { useEffect, useState } from "react";
import ContactEdit from "./ContactEdit";
import ContactView from "./ContactView";
import NoContactSelected from "./NoContactSelected";

const ContactArea = ({ contact, handleNew, handleSaveNew, handleCancelNew, scrollAreaHeight, scrollBarWidth }) => {
  const [editing, setEditing] = useState(false);

  useEffect(() => {
    setEditing(false);
  }, [contact])

  return (
    <div>
      {contact === undefined ?
      <NoContactSelected handleNew={handleNew} /> :
      <div>
        {
          contact.id === undefined ?
          <ContactEdit
            creating
            contact={contact}
            handleSaveNew={handleSaveNew}
            handleCancelNew={handleCancelNew}
            scrollAreaHeight={scrollAreaHeight}
          /> :
          (
            editing === true ?
            <ContactEdit
              contact={contact}
              setEditing={setEditing}
              scrollAreaHeight={scrollAreaHeight}
            /> :
            <ContactView
              contact={contact}
              setEditing={setEditing}
              handleNew={handleNew}
              scrollAreaHeight={scrollAreaHeight}
              scrollBarWidth={scrollBarWidth}
            />
          )
        }
      </div>}
    </div>
  );
};

export default ContactArea;