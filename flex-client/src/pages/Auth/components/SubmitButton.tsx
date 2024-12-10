import React from 'react';
import { Button } from '@mui/material';
import RefreshIcon from '@mui/icons-material/Refresh';

interface SubmitButtonProps {
  onClick: () => void;
  isLoading: boolean;
  text: string;
}

// 提交按鈕元件
export const SubmitButton: React.FC<SubmitButtonProps> = ({ onClick, isLoading, text }) => (
  <Button onClick={onClick} variant="contained" sx={{ borderRadius: '50px', backgroundColor: '#000000', padding: '10px' }} disabled={isLoading}>
    {isLoading ? <RefreshIcon /> : text}
  </Button>
);
