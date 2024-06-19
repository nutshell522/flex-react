import { createBrowserRouter } from 'react-router-dom';
import NotFound from './NotFound';
import Layout from './layout/Layout';
import ProductIndex from './page/Product/Index';

const router = createBrowserRouter([
  {
    path: '',
    element: <Layout />,
    children: [
      {
        path: '/w/:categoryId',
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
