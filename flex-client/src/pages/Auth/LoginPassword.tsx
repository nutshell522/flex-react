import { Button, Grid2, TextField } from '@mui/material';
import RefreshIcon from '@mui/icons-material/Refresh';
import { useMutation } from '@tanstack/react-query';
import { useDispatch } from 'react-redux';
import { loginSuccess } from '../../redux/authSlice';
import { IAuthData, ILoginReq, AuthApi, Response } from '../../utils/api';
import { useEffect, useState } from 'react';
import { Link, useLocation, useNavigate } from 'react-router-dom';
import { validateField, ValidationCondition } from '../../utils/validation';
import { AuthFormLayout } from './components/AuthFormLayout';

export const LoginPassword: React.FC = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const location = useLocation();
  const { email } = location.state || {};
  const [password, setPassword] = useState('');
  const [passwordError, setPasswordError] = useState('');
  const [passwordTouched, setPasswordTouched] = useState(false);
  const [isPasswordValid, setIsPasswordValid] = useState(false);

  useEffect(() => {
    if (!email) {
      // 如果 email 不存在，將用戶重定向到登入頁面
      navigate('/login');
    }
  }, [email, navigate]);

  // React Query mutation，用於發送登入請求
  const loginMutation = useMutation<Response<IAuthData>, Error, ILoginReq>({
    mutationFn: AuthApi.login,
    onSuccess: (response: Response<IAuthData>) => {
      const authData = response.data; // 提取數據
      dispatch(loginSuccess(authData)); // 將 token 和 user 信息存儲到 Redux

      alert('登入成功');
    },
    onError: (error: Error) => {
      console.error(error);
      // 可以在這裡顯示錯誤提示
    },
  });

  // password欄位驗證條件
  const passwordConditions: ValidationCondition[] = [
    {
      test: (value: string) => !!value, // 必填驗證
      errorMessage: '必填',
    },
  ];

  const validatePassword = () => {
    const { isValid, error } = validateField(password, passwordConditions);
    setIsPasswordValid(isValid);
    setPasswordError(error);
  };

  // password 輸入框失焦事件
  const blurHandler = () => {
    setPasswordTouched(true);
    validatePassword();
  };

  // password 輸入框鍵盤事件
  const keyUpHandler = () => {
    if (passwordTouched) {
      validatePassword();
    }
  };

  // password 輸入框改變事件
  const changeHandler = (e: React.ChangeEvent<HTMLInputElement>) => {
    setPassword(e.target.value);
    if (passwordTouched) {
      validatePassword();
    }
  };

  const handleLogin = () => {
    if (!isPasswordValid) return;
    loginMutation.mutate({ email, password });
  };

  return (
    <>
      <AuthFormLayout title="請輸入密碼。">
        {/* 語系選擇 */}
        <Grid2 size={12} sx={{ textAlign: 'left', marginBottom: '15px' }}>
          <p style={{ fontSize: '20px' }}>
            {email}
            <Link
              to="/login"
              state={{ emailInput: email }}
              style={{
                color: 'rgba(0, 0, 0, 0.6)',
                textDecoration: 'underline',
                textUnderlineOffset: '2px',
                marginLeft: '10px',
                fontSize: '18px',
              }}>
              編輯
            </Link>
          </p>
        </Grid2>
        <TextField
          sx={{ marginBottom: '20px' }}
          label="Password"
          type="password"
          variant="outlined"
          fullWidth
          margin="normal"
          onBlur={blurHandler}
          onChange={changeHandler}
          onKeyUp={keyUpHandler}
          value={password}
          error={!!passwordError}
          helperText={passwordError}
        />
        <Grid2 size={12} textAlign="left" sx={{ marginBottom: '20px' }}>
          <p style={{ color: 'rgba(0, 0, 0, 0.6)' }}>
            <Link
              to="#"
              style={{
                color: 'rgba(0, 0, 0, 0.6)',
                textDecoration: 'underline',
                textUnderlineOffset: '2px',
              }}>
              忘記密碼？
            </Link>
          </p>
        </Grid2>
        <Grid2 size={12} sx={{ display: 'flex', justifyContent: 'right' }}>
          <Button
            onClick={handleLogin}
            variant="contained"
            sx={{ borderRadius: '50px', backgroundColor: '#000000', padding: '10px' }}
            disabled={loginMutation.status === 'pending'}>
            {loginMutation.status === 'pending' ? <RefreshIcon /> : '登入'}
          </Button>
        </Grid2>
      </AuthFormLayout>
    </>
  );
};
