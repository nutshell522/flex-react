import { Link } from 'react-router-dom';
import styles from '../layout.module.scss';

const Header: React.FC = () => {
  return (
    <header className={`${styles['header']}`}>
      <div className={`${styles['header-left']}`}></div>
      <ul className={`${styles['header-right']}`}>
        <li className={`${styles['header-right-item']}`}>
          <Link to="/" className={`${styles['text-link']}`}>
            協助
          </Link>
        </li>
        <li className={`${styles['header-right-item']}`}>
          <Link to="/" className={`${styles['text-link']}`}>
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
