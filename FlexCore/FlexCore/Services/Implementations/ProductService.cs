using AutoMapper;
using FlexCore.Extentions;
using FlexCore.Models;
using FlexCore.Models.DTOs;
using FlexCore.Models.Entities;
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
                var productDtoPage = productEntityPage.MapTo<ProductEntity, ProductDto>(_mapper);
				return Result<Page<ProductDto>>.Success(productDtoPage);
            }
            catch (Exception ex)
            {
                return Result<Page<ProductDto>>.Failure(ex.Message);
            }
        }

        public async Task<Result<ProductDto>> GetProductByIdAsync(string id)
        {
            try
            {
                var productEntity = await _productRepository.GetProductByIdAsync(id);
                var productDto = _mapper.Map<ProductDto>(productEntity);
                return Result<ProductDto>.Success(productDto);
            }
            catch (Exception ex)
            {
                return Result<ProductDto>.Failure(ex.Message);
            }
        }
    }
}
