using AutoMapper;
using FlexCore.Models;
using FlexCore.Models.DTOs;
using FlexCore.Repositories.Interfaces;
using FlexCore.Services.Interfaces;

namespace FlexCore.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Result<Page<ProductDto>>> GetPageProductAsync(Pageable pageable, int? topCategoryId, int? middleCategoryId, int? bottomCategoryId, int? maxPrice, int? minPrice)
        {
            try
            {
                var productEntityPage = await _productRepository.GetProductsPageAsync(pageable, topCategoryId, middleCategoryId, bottomCategoryId, maxPrice, minPrice);
                var productDtos = _mapper.Map<IEnumerable<ProductDto>>(productEntityPage.Items).ToList();
                var productDtoPage = new Page<ProductDto>(productDtos, productEntityPage.TotalCount, productEntityPage.PageIndex, productEntityPage.PageSize);

                return Result<Page<ProductDto>>.Success(productDtoPage);
            }
            catch (Exception ex)
            {
                return Result<Page<ProductDto>>.Failure(ex.Message);
            }
        }
    }
}
