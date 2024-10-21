import { Button, Grid2, TextField } from '@mui/material';
import RefreshIcon from '@mui/icons-material/Refresh';
import { AuthFormLayout } from './components/AuthFormLayout';
import { Link, useLocation, useNavigate } from 'react-router-dom';
import { useEffect, useState } from 'react';
import { AuthApi, IRigisterReq, Response } from '../../utils/api';
import { useMutation } from '@tanstack/react-query';
import { validateField, ValidationCondition } from '../../utils/validation';

const Register: React.FC = () => {
  const navigate = useNavigate();
  const location = useLocation();
  const { email } = location.state || {};
  const [password, setPassword] = useState('');
  const [passwordError, setPasswordError] = useState('');
  const [passwordTouched, setPasswordTouched] = useState(false);
  const [isPasswordValid, setIsPasswordValid] = useState(false);
  const [name, setName] = useState('');

  // TODO 1. 密碼輸入時，在下面顯示密碼強度，驗證條件
  // 至少 8 個字元
  // 大寫字母、小寫字母以及 1 個數字
  // TODO 2. 訂閱勾選框 及 服務條款勾選框
  // 參考 https://accounts.nike.com/lookup?client_id=4fd2d5e7db76e0f85a6bb56721bd51df&redirect_uri=https://www.nike.com/auth/login&response_type=code&scope=openid%20nike.digital%20profile%20email%20phone%20flow%20country&state=fc4f20e16a9d4e32a7ae0ded9afdfab5&code_challenge=7i5LVCTOnJAx0Z0TtBJBCXCVSkQsDjIsjyRYtMKdLGI&code_challenge_method=S256

  useEffect(() => {
    if (!email) {
      // 如果 email 不存在，將用戶重定向到登入頁面
      navigate('/login');
    }
  }, [email, navigate]);

  // React Query mutation，用於發送登入請求
  const registerMutation = useMutation<Response<string>, Error, IRigisterReq>({
    mutationFn: AuthApi.register,
    onSuccess: (response: Response<string>) => {
      if (!response.data.isSuccess) return;
      alert('註冊成功，請去收取信件');
      navigate('/login', { state: { emailInput: email } });
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
    registerMutation.mutate({ email, password, name });
  };

  return (
    <>
      <AuthFormLayout title="馬上成為 Flex 會員。">
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
        {/* Name */}
        <TextField
          sx={{ marginBottom: '20px' }}
          label="姓名"
          type="text"
          variant="outlined"
          fullWidth
          margin="normal"
          onBlur={blurHandler}
          onChange={(e) => setName(e.target.value)}
          value={name}
          required
        />
        {/* Password */}
        <TextField
          sx={{ marginBottom: '20px' }}
          label="密碼"
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
          required
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
            disabled={registerMutation.status === 'pending'}>
            {registerMutation.status === 'pending' ? <RefreshIcon /> : '建立帳號'}
          </Button>
        </Grid2>
      </AuthFormLayout>
    </>
  );
};
export default Register;
