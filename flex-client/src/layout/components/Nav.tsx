import { Link } from 'react-router-dom';
import styles from '../layout.module.scss';
import {
  List as ListIcon,
  Heart as HeartIcon,
  Search as SearchIcon,
  Bag as BagIcon,
} from 'react-bootstrap-icons';
import useCategoryAPI, { TopCategory } from '../../hooks/topCategoryHook';

const Nav: React.FC = () => {
  const { topCategories } = useCategoryAPI();

  return (
    <>
      <nav className={styles['nav']}>
        <div className={styles['left']}>
          <Link to='/' className={styles['logo-wrapper']}>
            <img
              src='/LOGO/FlexLogoDark.png'
              alt=''
              className={`${styles['logo']}`}
            />
            <h1>FLEX</h1>
          </Link>
        </div>
        <div className={`${styles['center']}`}>
          <ul className={`${styles['center-ul']}`}>
            {topCategories.map((item: TopCategory) => (
              <li key={item.id} className={styles['nav-list-item']}>
                <Link to={`/${item.code}`} className={styles['nav-btn']}>
                  {item.name}
                </Link>
              </li>
            ))}
          </ul>

          <div className={styles['nav-dtail-wrapper']}></div>
        </div>

        <div className={styles['right']}>
          <div className={styles['search-wrapper']}>
            <input
              type='search'
              className={styles['search-bar']}
              placeholder='搜尋'
            />
            <SearchIcon className={styles['search-icon']} />
          </div>
          <Link
            to='#'
            onClick={(e) => e.preventDefault()}
            className={`${styles['nav-bar-btn']}`}
          >
            <HeartIcon className={styles['nav-bar-icon']} />
            <div className='count-favorites'></div>
          </Link>
          <Link
            to='#'
            onClick={(e) => e.preventDefault()}
            className={`${styles['nav-bar-btn']}`}
          >
            <BagIcon className={styles['nav-bar-icon']} />
            <div className='count-cart-items'></div>
          </Link>
          <Link
            to='#'
            onClick={(e) => e.preventDefault()}
            className={`${styles['nav-bar-btn']} ${styles['btn-list']}`}
          >
            <ListIcon className={styles['nav-bar-icon']} />
          </Link>
        </div>
      </nav>

      <div className={styles['nav-dtail-wrapper']}></div>

      <div className={styles['a']}></div>
    </>
  );
};

export default Nav;
