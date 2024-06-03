using FlexCore.Models;
using FlexCore.Models.DTOs;

namespace FlexCore.Services.Interfaces
{
    public interface IProductService
    {
        Task<Result<Page<ProductDto>>> GetPageProductAsync(Pageable pageable,
            int? topCategoryId,
            int? middleCategoryId,
            int? bottomCategoryId,
            int? maxPrice,
            int? minPrice);

        Task<Result<ProductDto>> GetProductByIdAsync(string id);
    }
}
