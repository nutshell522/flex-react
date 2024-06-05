using AutoMapper;
using FlexCore.Models.DTOs;
using FlexCore.Repositories.Interfaces;
using FlexCore.Services.Interfaces;

namespace FlexCore.Services.Implementations
{
	public class TopCategoryService : ITopCategoryService
	{
		private readonly ITopCategoryRepository _topCategoryRepository;
		private readonly IMapper _mapper;

		public TopCategoryService(ITopCategoryRepository topCategoryRepository, IMapper mapper)
		{
			_topCategoryRepository = topCategoryRepository;
			_mapper = mapper;
		}

		public async Task<IEnumerable<TopCategoryDto>> GetTopCategoriesAsync()
		{
			var topCategories = await _topCategoryRepository.GetTopCategoriesAsync();
			return _mapper.Map<IEnumerable<TopCategoryDto>>(topCategories);
		}
	}
}
