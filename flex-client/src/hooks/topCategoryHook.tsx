import { useEffect, useState } from 'react';
import { TopCategoryApi } from '../utils/api';

export interface TopCategory {
  id: number;
  name: string;
  code: string;
  middleCategories: MiddleCategory[];
}
export interface MiddleCategory {
  id: number;
  name: string;
  code: string;
  bottomCategories: BottomCategory[];
}
export interface BottomCategory {
  id: number;
  name: string;
  code: string;
}

const useCategoryAPI = () => {
  const [topCategories, setTopCategories] = useState<TopCategory[]>([]);
  const [middleCategories, setMiddleCategories] = useState<{
    [key: string]: MiddleCategory;
  }>({});
  useEffect(() => {
    TopCategoryApi.get().then((res) => {
      setTopCategories(res.data);
    });
  }, []);

  useEffect(() => {
    const middleCategories: { [key: string]: MiddleCategory } = {};
    topCategories.forEach((topCategory) => {
      topCategory.middleCategories.forEach((middleCategory) => {
        middleCategories[middleCategory.code] = middleCategory;
      });
    });
    setMiddleCategories(middleCategories);
  }, [topCategories]);

  return { topCategories, middleCategories };
};

export default useCategoryAPI;
