import { Outlet } from 'react-router-dom';
import Header from './components/Header';
import Nav from './components/Nav';
import { LoaderProvider } from '../contexts/LoderContext';

const Layout: React.FC = () => {
  return (
    <LoaderProvider>
      <Header />
      <Nav />
      <Outlet />
    </LoaderProvider>
  );
};

export default Layout;
