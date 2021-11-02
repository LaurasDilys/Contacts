const _checkLength = (inputText, maxLength) => {
  return maxLength !== undefined ? inputText.length <= maxLength : true;
};

const _checkRegex = (inputText, regexString, regexRuleReverse) => {
  if (regexString === undefined) {
    return true;
  }
  const regexValid = new RegExp(regexString).test(inputText);
  return regexRuleReverse ? !regexValid : regexValid;
};

// This function is used to validate rules, and if needed apply strict rules (e.g. don't allow input after max length)
const _validateInput = (
  e,
  check,
  checkParams,
  strict,
  oldValue
) => {
  // If input is valid return that input is valid
  if (check(e.target.value, ...checkParams)) {
    return true;
  }
  // If input is not valid, and strict validation is on, set input to previous valid input, and also return that input is valid
  if (strict) {
    e.target.value = oldValue;
    return true;
  }
  return false;
};

export const validateStringLength = (
  e,
  maxLength,
  strictLength
) => {
  return typeof e.target.value === 'string'
    ? _validateInput(e, _checkLength, [maxLength], strictLength, e.target.value.substring(0, maxLength))
    : true;
};

export const validateRegex = (
  e,
  regexString,
  regexRuleReverse,
  strictRegex,
  oldValue
) => {
  return _validateInput(e, _checkRegex, [regexString, regexRuleReverse], strictRegex, oldValue);
};

export const validateAdditionalRules = (
  e,
  additionalCheck,
  additionalStrict,
  oldValue
) => {
  return additionalCheck !== undefined ? _validateInput(e, additionalCheck, [], additionalStrict, oldValue) : true;
};