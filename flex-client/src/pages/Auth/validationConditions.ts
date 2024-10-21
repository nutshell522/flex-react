import { ValidationCondition } from '../../utils/validation';

// email 驗證條件
export const emailConditions: ValidationCondition[] = [
  {
    test: (value: string) => !!value, // 必填
    errorMessage: '必填',
  },
  {
    test: (value: string) => /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/.test(value), // 電子郵件格式
    errorMessage: '請輸入有效的電子郵件地址',
  },
];

// 密碼驗證條件
export const passwordConditions: ValidationCondition[] = [
  {
    test: (value: string) => !!value, // 必填
    errorMessage: '必填',
  },
  // {
  //   test: (value: string) => value.length >= 8, // 密碼長度
  //   errorMessage: '密碼長度必須至少8個字元',
  // },
];
