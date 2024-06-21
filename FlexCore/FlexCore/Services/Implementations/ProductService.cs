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
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _repo = productRepository;
            _mapper = mapper;
        }

        public async Task<Page<ProductDto>> GetPageProductAsync(Pageable pageable, int? topCategoryId, int? middleCategoryId, int? bottomCategoryId,string name, int? maxPrice, int? minPrice)
        {
            var productEntityPage = await _repo.GetProductsPageAsync(pageable, topCategoryId, middleCategoryId, bottomCategoryId, name,maxPrice, minPrice); 
            var productDtoPage = productEntityPage.MapTo<ProductEntity, ProductDto>(_mapper);
			return productDtoPage;
        }

        public async Task<ProductDto> GetProductByIdAsync(string id)
        {
            var productEntity = await _repo.GetProductByIdAsync(id);
            var productDto = _mapper.Map<ProductDto>(productEntity);
            return productDto;
        }
    }
}
