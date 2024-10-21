import React from 'react';
import { useMutation } from '@tanstack/react-query';
import { Grid2, Link } from '@mui/material';
import { Response, AuthApi } from '../../utils/api';
import { AuthFormLayout } from './components/AuthFormLayout';
import { useInputField } from '../../hooks/useInputField';
import { ValidatedTextField } from './components/ValidatedTextField'; // 抽出的輸入框元件
import { SubmitButton } from './components/SubmitButton'; // 抽出的按鈕元件
import { emailConditions } from './validationConditions'; // 驗證條件
import { useLocation, useNavigate } from 'react-router-dom';

enum emailStatus {
  EXIST_AND_NOT_VERIFIED = 0,
  EXIST_AND_VERIFIED = 1,
  NOT_EXIST = 2,
}

const Login: React.FC = () => {
  const navigate = useNavigate();
  const location = useLocation();
  const { emailInput } = location.state || {};

  const emailField = useInputField(emailInput || '', emailConditions);

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
        case emailStatus.EXIST_AND_NOT_VERIFIED:
          alert('請前往驗證您的電子郵件地址');
          break;
        case emailStatus.EXIST_AND_VERIFIED:
          navigate('/password', { state: { email: emailField.value } });
          break;
        case emailStatus.NOT_EXIST:
          navigate('/register', { state: { email: emailField.value } });
          break;
        default:
          break;
      }
    },
    onError: (error: Error) => {
      alert(error.message);
    },
  });

  const handleSubmit = () => {
    emailField.validate();
    if (!emailField.isValid) return;
    checkEmailStatusMutation.mutate(emailField.value);
  };

  return (
    <AuthFormLayout title="輸入你的電子郵件即可註冊或登入。">
      <Grid2 size={12} sx={{ textAlign: 'left', marginBottom: '15px' }}>
        <p>
          台灣
          <Link href="#" underline="always" color="textSecondary" sx={{ marginLeft: '10px' }}>
            變更
          </Link>
        </p>
      </Grid2>

      <ValidatedTextField
        label="電子郵件*"
        type="email"
        value={emailField.value}
        error={emailField.error}
        onBlur={emailField.handleBlur}
        onChange={emailField.handleChange}
      />

      <Grid2 size={12} sx={{ display: 'flex', justifyContent: 'right' }}>
        <SubmitButton
          onClick={handleSubmit}
          isLoading={checkEmailStatusMutation.status === 'pending'}
          text="繼續"
        />
      </Grid2>
    </AuthFormLayout>
  );
};

export default Login;
