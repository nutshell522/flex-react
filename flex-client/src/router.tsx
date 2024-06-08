import { createBrowserRouter } from 'react-router-dom';
import NotFound from './NotFound';
import Layout from './layout/Layout';

const router = createBrowserRouter([
  {
    path: '',
    element: <Layout />,
    children: [
      {
        path: '*',
        element: <NotFound />,
      },
    ],
  },
]);

export default router;
