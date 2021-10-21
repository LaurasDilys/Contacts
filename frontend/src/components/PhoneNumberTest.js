import PhoneInput from 'react-phone-input-2'
import 'react-phone-input-2/lib/material.css'
import { useEffect, useRef, useState } from 'react';
import { Button, TextField } from '@mui/material';

const initialNumber = '';

const PhoneNumberTest = () => {
  const [value, setValue] = useState(initialNumber === '' ? null : initialNumber);
  const [formattedValue, setFormattedValue] = useState();

  const handleChange = (value, data, event, formattedValue) => {
    setValue(value);
    setFormattedValue(formattedValue);
  }

  //___
  //__
  //_
  //
  const ref = useRef(null);
  useEffect(() => {
    // value !== null && value !== '' &&  ( <= )  unnecessary, when country isn't set to 'lt'
    setFormattedValue(ref.current.state.formattedNumber)
  }, [ref])
  //
  //_
  //__
  //___
  
  return (
    <div style={{ margin: 200, width: 500}}>

      <PhoneInput
        // style={{ display: 'none' }}
        // value='37061417706'
        // masks={{lt: '(...) .....'}}

        ref={ref}

        country='lt'
        value={value}
        onChange={handleChange}
        masks={{lt: '(...) .....'}}
      />

      <TextField />

      <h1>Value: {value}</h1>
      <h1>Formated: {formattedValue}</h1>

      <Button variant='contained' onClick={() => setValue()}>Reset</Button>

    </div>
  );
};

export default PhoneNumberTest;