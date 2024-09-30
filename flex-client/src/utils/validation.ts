export type ValidationCondition = {
  test: (value: string) => boolean;
  errorMessage: string;
};

export const validateField = (
  value: string,
  conditions: ValidationCondition[]
): { isValid: boolean; error: string } => {
  for (const condition of conditions) {
    if (!condition.test(value)) {
      return { isValid: false, error: condition.errorMessage };
    }
  }
  return { isValid: true, error: '' };
};
