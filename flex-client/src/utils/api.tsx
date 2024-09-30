import axios, { AxiosResponse } from 'axios';
import config from '../config.tsx';
import { TopCategory } from '../hooks/topCategoryHook.tsx';
import { IProductIndex } from '../hooks/productHook.tsx';
import { store } from '../store.tsx';

export interface IApiResult<T> {
  data: T;
  message: string;
  isSuccess: boolean;
}

export type Response<T> = AxiosResponse<IApiResult<T>>;

export type ApiResult<T> = Promise<Response<T>>;

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

export interface IAuthData {
  token: string;
  user: IUser;
}

export interface IUser {
  id: number;
  email: string;
  name: string;
}

export interface ILoginReq {
  email: string;
  password: string;
}

export interface IConfirmEmailReq {
  token: string;
  email: string;
}

const axiosInstance = axios.create({
  baseURL: config.baseUrl,
});

axiosInstance.interceptors.request.use((config) => {
  const token = store.getState().auth.token;
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  } else {
    delete config.headers.Authorization;
  }
  return config;
});

export class TopCategoryApi {
  private static readonly path = '/TopCategory';
  static get = (): Promise<AxiosResponse<TopCategory[]>> => axiosInstance.get(this.path);
}

export class ProductApi {
  private static readonly path = '/Products';
  static search = (
    searchParam: IProductSearchParamReq
  ): Promise<AxiosResponse<IPage<IProductIndex>>> =>
    axiosInstance.post(`${this.path}/search`, searchParam);
  static getById = (id: number): Promise<AxiosResponse<IProductIndex>> =>
    axiosInstance.get(`${this.path}/${id}`);
}

export class AuthApi {
  private static readonly path = '/Auth';
  static login = (data: ILoginReq): ApiResult<IAuthData> =>
    axiosInstance.post(`${this.path}/login`, data);
  static register = (data: IUser): ApiResult<string> =>
    axiosInstance.post(`${this.path}/register`, data);
  static confirmEmail = (data: IConfirmEmailReq): ApiResult<void> =>
    axiosInstance.get(`${this.path}/confirmEmail?token=${data.token}&email=${data.email}`);
  static checkEmailStatus = (email: string): ApiResult<number> =>
    axiosInstance.get(`${this.path}/checkEmailStatus?email=${email}`);
}
