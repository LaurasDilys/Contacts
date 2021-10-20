import PhoneInput from 'react-phone-input-2'
import 'react-phone-input-2/lib/material.css'
import { useState } from 'react';
import { Button, TextField } from '@mui/material';

const initialNumber = '';

const PhoneNumberTest = () => {
  const [value, setValue] = useState(initialNumber === '' ? null : initialNumber);
  
  return (
    <div style={{ margin: 200, width: 500}}>

      <PhoneInput
        country='lt'
        value={value}
        onChange={setValue}
      />

      <TextField />

      <h1>Value: {value}</h1>

      <Button variant='contained' onClick={() => setValue()}>Reset</Button>

    </div>
  );
};

export default PhoneNumberTest;