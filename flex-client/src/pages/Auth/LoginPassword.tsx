import React, { useEffect } from 'react';
import { useMutation } from '@tanstack/react-query';
import { Grid2 } from '@mui/material';
import { IAuthData, ILoginReq, AuthApi, Response } from '../../utils/api';
import { useInputField } from '../../hooks/useInputField';
import { ValidatedTextField } from './components/ValidatedTextField'; // 抽出的 TextField 元件
import { SubmitButton } from './components/SubmitButton'; // 抽出的按鈕元件
import { passwordConditions } from './validationConditions'; // 驗證條件
import { Link, useLocation, useNavigate } from 'react-router-dom';
import { AuthFormLayout } from './components/AuthFormLayout';

const LoginPassword: React.FC = () => {
  const navigate = useNavigate();
  const location = useLocation();
  const { email } = location.state || {};

  const passwordField = useInputField('', passwordConditions);

  useEffect(() => {
    if (!email) {
      navigate('/login');
    }
  }, [email, navigate]);

  // React Query mutation，用於發送登入請求
  const loginMutation = useMutation<Response<IAuthData>, Error, ILoginReq>({
    mutationFn: AuthApi.login,
    onSuccess: (response: Response<IAuthData>) => {
      const { isSuccess, data, message } = response.data;
      if (!isSuccess) {
        alert(message);
        return;
      }
      localStorage.setItem('flex_token', data.token);
      localStorage.setItem('flex_user', JSON.stringify(data.user));
      navigate('/');
    },
    onError: (error: Error) => {
      console.error(error);
    },
  });

  const handleLogin = () => {
    passwordField.validate();
    if (!passwordField.isValid) return;
    loginMutation.mutate({ email, password: passwordField.value });
  };

  return (
    <AuthFormLayout title="請輸入密碼。">
      <Grid2 size={12} sx={{ textAlign: 'left', marginBottom: '15px' }}>
        <p>
          {email}
          <Link to="/login" state={{ emailInput: email }} style={{ marginLeft: '10px' }}>
            編輯
          </Link>
        </p>
      </Grid2>

      <ValidatedTextField
        label="密碼"
        type="password"
        value={passwordField.value}
        error={passwordField.error}
        onBlur={passwordField.handleBlur}
        onChange={passwordField.handleChange}
      />
      <Grid2 size={12} sx={{ display: 'flex', justifyContent: 'right' }}>
        <SubmitButton
          onClick={handleLogin}
          isLoading={loginMutation.status === 'pending'}
          text="登入"
        />
      </Grid2>
    </AuthFormLayout>
  );
};

export default LoginPassword;
