import React, { useEffect, useState } from 'react';
import { useMutation } from '@tanstack/react-query';
import { TextField, Button, Grid2, Link } from '@mui/material';
import { Response, AuthApi } from '../../utils/api';
import { AuthFormLayout } from './components/AuthFormLayout';
import RefreshIcon from '@mui/icons-material/Refresh';
import { validateField, ValidationCondition } from '../../utils/validation';
import { useLocation, useNavigate } from 'react-router-dom';

// email 狀態
enum emailStatus {
  // email 存在但未驗證
  EXIST_AND_NOT_VERIFIED = 0,
  // email 存在且已驗證
  EXIST_AND_VERIFIED = 1,
  // email 不存在
  NOT_EXIST = 2,
}

export const Login: React.FC = () => {
  const navigate = useNavigate();
  const location = useLocation();
  const { emailInput } = location.state || {};
  const [email, setEmail] = useState<string>('');
  const [emailTouched, setEmailTouched] = useState<boolean>(false); // 是否觸碰過 email 輸入框
  const [isEmailValid, setIsEmailValid] = useState<boolean>(false); // email 是否合法
  const [emailError, setEmailError] = useState<string>(''); // email 錯誤提示

  useEffect(() => {
    if (emailInput) {
      setEmail(emailInput);
    }
  }, [emailInput]);

  // React Query mutation，用於檢查 email 狀態
  const checkEmailStatusMutation = useMutation<Response<number>, Error, string>({
    mutationFn: AuthApi.checkEmailStatus,
    onSuccess: (response: Response<number>) => {
      const { isSuccess, data, message } = response.data;
      if (!isSuccess) {
        alert(message);
        return;
      }

      switch (data) {
        // email 存在但未驗證
        case emailStatus.EXIST_AND_NOT_VERIFIED:
          alert('請前往驗證您的電子郵件地址');
          break;
        // email 存在且已驗證 跳轉到輸入密碼頁面
        case emailStatus.EXIST_AND_VERIFIED:
          navigate('password', { state: { email } });
          break;
        // email 不存在
        case emailStatus.NOT_EXIST:
          alert('註冊');
          // 跳轉到註冊頁面
          break;
        default:
          break;
      }
    },
    onError: (error: Error) => {
      alert(error.message);
    },
  });

  // email 驗證條件
  const emailConditions: ValidationCondition[] = [
    {
      test: (value: string) => !!value, // 必填驗證
      errorMessage: '必填',
    },
    {
      test: (value: string) => /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/.test(value), // 電子郵件格式驗證
      errorMessage: '請輸入有效的電子郵件地址',
    },
  ];

  const validateEmail = () => {
    const { isValid, error } = validateField(email, emailConditions);
    setIsEmailValid(isValid);
    setEmailError(error);
  };

  // email 輸入框失焦事件
  const blurHandler = () => {
    setEmailTouched(true);
    validateEmail();
  };

  // email 輸入框改變事件
  const changeHandler = (e: React.ChangeEvent<HTMLInputElement>) => {
    setEmail(e.target.value);
    if (emailTouched) {
      validateEmail();
    }
  };

  // 表單提交邏輯
  const handleSubmit = () => {
    validateEmail();
    if (!isEmailValid) return;

    checkEmailStatusMutation.mutate(email);
  };

  return (
    <AuthFormLayout
      title="
        輸入你的電子郵件即可註冊或登入。">
      {/* 語系選擇 */}
      <Grid2 size={12} sx={{ textAlign: 'left', marginBottom: '15px' }}>
        <p>
          台灣
          <Link href="#" underline="always" color="textSecondary" sx={{ marginLeft: '10px' }}>
            變更
          </Link>
        </p>
      </Grid2>
      {/* Email */}
      <TextField
        label="電子郵件*"
        type="email"
        variant="outlined"
        fullWidth
        margin="normal"
        value={email}
        onBlur={blurHandler}
        onChange={changeHandler}
        error={!!emailError}
        helperText={emailError}
        sx={{ marginBottom: '40px', outline: 'none', border: 'none' }}
      />
      {/* 隱私政策 */}
      <Grid2 size={12} textAlign="left" sx={{ marginBottom: '20px' }}>
        <p style={{ color: 'rgba(0, 0, 0, 0.6)' }}>
          繼續即代表我同意 Flex 的
          <Link href="#" underline="always" color="textSecondary">
            隱私政策
          </Link>
          與
          <Link href="#" underline="always" color="textSecondary">
            使用條款
          </Link>
        </p>
      </Grid2>
      <Grid2 size={12} sx={{ display: 'flex', justifyContent: 'right' }}>
        <Button
          onClick={handleSubmit}
          variant="contained"
          sx={{ borderRadius: '50px', backgroundColor: '#000000', padding: '10px' }}
          disabled={checkEmailStatusMutation.status === 'pending'}>
          {checkEmailStatusMutation.status === 'pending' ? <RefreshIcon /> : '繼續'}
        </Button>
      </Grid2>
    </AuthFormLayout>
  );
};
