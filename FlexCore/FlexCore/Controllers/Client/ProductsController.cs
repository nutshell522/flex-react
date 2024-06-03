using AutoMapper;
using FlexCore.Extentions;
using FlexCore.Models;
using FlexCore.Models.DTOs;
using FlexCore.Models.ViewModels.ClientViewModels.Product;
using FlexCore.Services.Interfaces;
using Microsoft.AspNetCore.Http;
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
		public async Task<IActionResult> SearchProducts([FromBody] ProductListSearchCriteria criteria)
		{
			var result = await _productService.GetPageProductAsync(
				criteria.Pageable,
				criteria.TopCategoryId,
				criteria.MiddleCategoryId,
				criteria.BottomCategoryId,
				criteria.MaxPrice,
				criteria.MinPrice
			);

			if (!result.IsSuccess)
			{
				return BadRequest(result.Message);
			}

			var productViewModelPage = result.Data.MapTo<ProductDto, ProductListVM>(_mapper);
			return Ok(productViewModelPage);
		}



		[HttpGet("{id}")]
		public async Task<IActionResult> GetProductById(string id)
        {
            var result = await _productService.GetProductByIdAsync(id);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
	}
}
