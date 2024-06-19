import axios, { AxiosResponse } from 'axios';
import config from '../config.tsx';
import { TopCategory } from '../hooks/topCategoryHook.tsx';
import { IProductIndex } from '../hooks/productHook.tsx';

interface IPage<T> {
  items: T[];
  pageIndex: number;
  pageSize: number;
  totalPages: number;
  totalCount: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
}

export interface IPageable {
  page: number;
  size: number;
  sort?: ISort[];
}

export interface ISort {
  by: string;
  direction: string;
}

export interface IProductSearchParamReq {
  pageable?: IPageable;
  topCategoryId?: number;
  middleCategoryId?: number;
  bottomCategoryId?: number;
  name?: string;
  maxPrice?: number;
  minPrice?: number;
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
  private static readonly path = '/Products';
  static search = (searchParam: IProductSearchParamReq): Promise<AxiosResponse<IPage<IProductIndex>>> =>
    axiosInstance.post(`${this.path}/search`, searchParam);
  static getById = (id: number): Promise<AxiosResponse<IProductIndex>> =>
    axiosInstance.get(`${this.path}/${id}`);
}
