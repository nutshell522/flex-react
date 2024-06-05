using AutoMapper;
using FlexCore.Models.ViewModels.Client.MiddleCategory;
using FlexCore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FlexCore.Controllers.Client
{
	[Route("api/client/[controller]")]
	[ApiController]
	public class MiddleCategoryController: ControllerBase
	{
		private readonly IMiddleCategoryService _middleCategoryService;
		private readonly IMapper _mapper;

		public MiddleCategoryController(IMiddleCategoryService middleCategoryService, IMapper mapper)
		{
			_middleCategoryService = middleCategoryService;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> GetMiddleCategories(int topCategoryId)
		{
			var middleCategories = await _middleCategoryService.GetMiddleCategoriesAsync(topCategoryId);
			var vm = _mapper.Map<IEnumerable<MiddleCategoryVM>>(middleCategories);
			return Ok(vm);
		}

	}
}
