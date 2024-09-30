import { Grid2 } from '@mui/material';

interface IAuthFormLayoutProps {
  title: string;
  children: React.ReactNode;
}

// 用於登入和註冊頁面的表單布局
export const AuthFormLayout: React.FC<IAuthFormLayoutProps> = ({ children, title }) => {
  return (
    <>
      {/* 表單外層容器 */}
      <Grid2 container sx={{ width: '40%', minWidth: '460px', margin: 'auto', maxWidth: '460px' }}>
        {/* Logo */}
        <Grid2
          container
          justifyContent="start"
          alignItems="center"
          size={12}
          sx={{ height: '100px' }}>
          <img src="/LOGO/FlexLogoDark.png" alt="" style={{ width: '80px' }} />
        </Grid2>
        {/* 表單標題 */}
        <Grid2 size={12}>
          <h2
            style={{
              marginBottom: '15px',
              fontSize: '28px',
              textAlign: 'left',
              fontWeight: '400',
            }}>
            {title}
          </h2>
        </Grid2>
        {children}
      </Grid2>
    </>
  );
};
