using AutoMapper;
using FlexCore.Models.ViewModels.Client.TopCategory;
using FlexCore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FlexCore.Controllers.Client
{
	[Route("api/client/[controller]")]
	[ApiController]
	public class TopCategoryController : ControllerBase
	{
		private readonly ITopCategoryService topCategoryService;
		private readonly IMapper _mapper;

		public TopCategoryController(ITopCategoryService topCategoryService, IMapper mapper)
		{
			this.topCategoryService = topCategoryService;
			_mapper = mapper;
		}
		[HttpGet]
		public async Task<IActionResult> GetTopCategories()
		{
			var topCategories = await topCategoryService.GetTopCategoriesAsync();
			var vm = _mapper.Map<IEnumerable<TopCategoryVM>>(topCategories);
			return Ok(vm);
		}
	}
}
