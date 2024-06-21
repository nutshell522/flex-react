using AutoMapper;
using FlexCore.Extentions;
using FlexCore.Models.DTOs;
using FlexCore.Models.ViewModels.Client.Product;
using FlexCore.Models.ViewModels.ClientViewModels.Product;
using FlexCore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FlexCore.Controllers.Client
{
	[Route("api/client/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{

		private readonly IProductService _productService;
		private readonly IMapper _mapper;

		public ProductsController(IProductService productService, IMapper mapper)
		{
			_productService = productService;
			_mapper = mapper;
		}
		[HttpPost("search")]
		public async Task<IActionResult> SearchProducts([FromBody] ProductPageSearchCriteria criteria)
		{
			var result = await _productService.GetPageProductAsync(
				criteria.Pageable,
				criteria.TopCategoryId,
				criteria.MiddleCategoryId,
				criteria.BottomCategoryId,
				criteria.Name,
				criteria.MaxPrice,
				criteria.MinPrice
			);

			var vm = result.MapTo<ProductDto, ProductListVM>(_mapper);
			return Ok(vm);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetProductById(string id)
        {
            var result = await _productService.GetProductByIdAsync(id);

			var vm = _mapper.Map<ProductDto, ProductVM>(result);

			return Ok(vm);
        }
	}
}
