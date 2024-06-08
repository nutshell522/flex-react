import axios, { AxiosResponse } from 'axios';
import config from '../config.tsx';
import { TopCategory } from '../hooks/topCategoryHook.tsx';

const axiosInstance = axios.create({
  baseURL: config.baseUrl,
});

export class TopCategoryApi {
  private static readonly path = '/TopCategory';
  static get = (): Promise<AxiosResponse<TopCategory[]>> =>
    axiosInstance.get(this.path);
}
