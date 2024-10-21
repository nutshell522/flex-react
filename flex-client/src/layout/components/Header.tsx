import { Link, useNavigate } from 'react-router-dom';
import styles from '../layout.module.scss';
import { Person } from 'react-bootstrap-icons';

const Header: React.FC = () => {
  const navigate = useNavigate();
  const isAuthenticated = localStorage.getItem('flex_token');
  const user = JSON.parse(localStorage.getItem('flex_user') || '{}');

  const handleLogout = () => {
    localStorage.removeItem('flex_token');
    localStorage.removeItem('flex_user');
    navigate('');
    window.location.href = '/login';
  };

  return (
    <header className={`${styles['header']}`}>
      <div className={`${styles['header-left']}`}></div>
      <ul className={`${styles['header-right']}`}>
        <li className={`${styles['header-right-item']}`}>
          <Link to="/" className={`${styles['text-link']}`}>
            協助
          </Link>
        </li>
        {isAuthenticated && user ? (
          <>
            <li className={`${styles['header-right-item']}`}>
              <Link to="/profile" className={`${styles['text-link']}`}>
                <Person />
                {user.name}
              </Link>
            </li>
            <li className={`${styles['header-right-item']}`}>
              <Link to="#" onClick={handleLogout} className={`${styles['text-link']}`}>
                登出
              </Link>
            </li>
          </>
        ) : null}
        <li className={`${styles['header-right-item']}`}>
          <Link to="/login" className={`${styles['text-link']}`}>
            加入
          </Link>
        </li>
        <li className={`${styles['header-right-item']}`}>
          <Link to="/login" className={`${styles['text-link']}`}>
            登入
          </Link>
        </li>
      </ul>
    </header>
  );
};

export default Header;
