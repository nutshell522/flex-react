import { createBrowserRouter } from 'react-router-dom';
import NotFound from './NotFound';
import Layout from './layout/Layout';
import ProductIndex from './pages/Product/Index';
import Login from './pages/Auth/Login';
import LoginPassword from './pages/Auth/LoginPassword';
import Register from './pages/Auth/Register';

const router = createBrowserRouter([
  {
    path: 'login',
    element: <Login />,
  },
  {
    path: 'password',
    element: <LoginPassword />,
  },
  {
    path: 'register',
    element: <Register />,
  },
  {
    path: '',
    element: <Layout />,
    children: [
      {
        path: 'w/:categoryId', // 這裡也是相對路徑
        element: <ProductIndex />,
      },
      {
        path: '*',
        element: <NotFound />,
      },
    ],
  },
]);

export default router;
