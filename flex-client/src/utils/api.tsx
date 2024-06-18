import axios, { AxiosResponse } from 'axios';
import config from '../config.tsx';
import { TopCategory } from '../hooks/topCategoryHook.tsx';
import { IProductIndex } from '../page/Product/components/Products.tsx';

interface Page<T> {
  content: T[];
  totalPages: number;
  totalElements: number;
  last: boolean;
  size: number;
  number: number;
  sort: unknown;
  first: boolean;
  numberOfElements: number;
  empty: boolean;
}

const axiosInstance = axios.create({
  baseURL: config.baseUrl,
});

export class TopCategoryApi {
  private static readonly path = '/TopCategory';
  static get = (): Promise<AxiosResponse<TopCategory[]>> =>
    axiosInstance.get(this.path);
}

export class ProductApi {
  private static readonly path = '/Product';
  static get = (): Promise<AxiosResponse<Page<IProductIndex>>> =>
    axiosInstance.get(this.path);
}
