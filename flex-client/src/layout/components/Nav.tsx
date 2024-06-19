import { Link } from 'react-router-dom';
import styles from '../layout.module.scss';
import {
  List as ListIcon,
  Heart as HeartIcon,
  Search as SearchIcon,
  Bag as BagIcon,
} from 'react-bootstrap-icons';
import useCategoryAPI, { TopCategory } from '../../hooks/topCategoryHook';
import { useState } from 'react';

const Nav: React.FC = () => {
  const [activeNavDetail, setActiveNavDetail] = useState<number | null>(null);
  const { topCategories } = useCategoryAPI();

  const handleMouseEnter = (id: number) => {
    setActiveNavDetail(id);
  };

  const handleMouseLeave = (id: number) => {
    setActiveNavDetail((prevValue) => (prevValue === id ? null : prevValue));
  };

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
              <li
                key={item.id}
                className={styles['nav-list-item']}
                onMouseEnter={() => handleMouseEnter(item.id)}
                onMouseLeave={() => handleMouseLeave(item.id)}
              >
                <Link
                  to={`/w/${item.code}-${item.id}`}
                  className={styles['nav-btn']}
                  state={{ topCategoryId: item.id }}
                >
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
      {topCategories.map((item: TopCategory) => (
        <div
          key={item.id}
          className={`${styles['nav-detail-wrapper']} ${activeNavDetail === item.id ? styles['active'] : ''
            }`}
          onMouseEnter={() => handleMouseEnter(item.id)}
          onMouseLeave={() => handleMouseLeave(item.id)}
        >
          <div className={styles['nav-detail-area']}>
            {item.middleCategories.map((middlecategory) => (
              <div
                key={middlecategory.id}
                className={styles['nav-detail-block']}
              >
                <Link to={`/w/${item.code}-${item.id}-${middlecategory.id}`}>
                  <h3 className={styles['nav-detail-item-title']}>
                    {middlecategory.name}
                  </h3>
                </Link>
                <ul className={styles['nav-detail-item-ul']}>
                  {middlecategory.bottomCategories.map((bottomcategory) => (
                    <li
                      key={bottomcategory.id}
                      className={styles['nav-detail-item-li']}
                    >
                      <Link
                        to={`/w/${item.code}-${item.id}-${middlecategory.id}-${bottomcategory.id}`}
                      >
                        {bottomcategory.name}
                      </Link>
                    </li>
                  ))}
                </ul>
              </div>
            ))}
          </div>
        </div>
      ))}
      {/* <div className={styles['a']}></div> */}
    </>
  );
};

export default Nav;
