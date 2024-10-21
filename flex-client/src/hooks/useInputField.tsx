// useInputField.ts
import { useState, useEffect, useCallback } from 'react';
import { validateField, ValidationCondition } from '../utils/validation';

export const useInputField = (initialValue: string, conditions: ValidationCondition[]) => {
  const [value, setValue] = useState(initialValue);
  const [touched, setTouched] = useState(false);
  const [isValid, setIsValid] = useState(false);
  const [error, setError] = useState('');

  const validate = useCallback(() => {
    const { isValid, error } = validateField(value, conditions);
    setIsValid(isValid);
    setError(error);
  }, [value, conditions]);

  useEffect(() => {
    if (touched) {
      validate();
    }
  }, [value, touched, validate]);

  const handleBlur = () => setTouched(true);
  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => setValue(e.target.value);

  return {
    value,
    error,
    isValid,
    handleBlur,
    handleChange,
    validate,
  };
};
