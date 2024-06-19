const baseUrl =
  import.meta.env.VITE_REACT_APP_API_URL || 'http://localhost:8080/api/client';
const baseImageUrl =
  import.meta.env.VITE_REACT_APP_IMAGE_URL || 'http://localhost:8080/Public/Images';

export default {
  baseUrl,
  baseImageUrl,
};
