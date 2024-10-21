import React from 'react';
import { TextField } from '@mui/material';

interface ValidatedTextFieldProps {
  label: string;
  type: string;
  value: string;
  error: string;
  onBlur: () => void;
  onChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
}

// 驗證輸入框元件
export const ValidatedTextField: React.FC<ValidatedTextFieldProps> = ({
  label,
  type,
  value,
  error,
  onBlur,
  onChange,
}) => (
  <TextField
    label={label}
    type={type}
    variant="outlined"
    fullWidth
    margin="normal"
    value={value}
    onBlur={onBlur}
    onChange={onChange}
    error={!!error}
    helperText={error}
    sx={{ marginBottom: '20px', outline: 'none', border: 'none' }}
  />
);
