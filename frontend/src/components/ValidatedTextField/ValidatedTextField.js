import { useState, ChangeEvent } from 'react';
import { TextField } from '@mui/material';

import { validateStringLength, validateRegex, validateAdditionalRules } from './ValidationHelpers';

const ValidatedTextField = ({
  validationProps: {
    maxLength,
    strictLength = false,
    regexString,
    strictRegex = false,
    regexRuleReverse = false,
    isValid,
    setIsValid,
    isDirty,
    setIsDirty,
    additionalCheck,
    additionalStrict = false,
  },
  onChange,
  value,
  ...textFieldProps
}) => {
  const [isFocused, setIsFocused] = useState(false);

  const isErroneous = !isValid && (isDirty !== undefined ? isDirty : true) && value !== '';

  const onFocus = () => setIsFocused(true);
  const onBlur = () => setIsFocused(false);

  const onInputChange = (e) => {
    if (setIsDirty !== undefined) {
      setIsDirty(true);
    }
    // checking is done in a single expression with functions to improve performance
    // empty is the second, because we want to check the user given rules first
    setIsValid(
      validateAdditionalRules(e, additionalCheck, additionalStrict, value) &&
        e.target.value !== '' &&
        validateStringLength(e, maxLength, strictLength) &&
        validateRegex(e, regexString, regexRuleReverse, strictRegex, value)
    );
    if (onChange !== undefined) {
      onChange(e);
    }
  };

  return (
    <TextField
      error={isErroneous && !isFocused}
      FormHelperTextProps={{ error: isErroneous }}
      onBlur={onBlur}
      onChange={onInputChange}
      onFocus={onFocus}
      required
      value={value}
      {...textFieldProps}
    />
  );
};

export default ValidatedTextField;
