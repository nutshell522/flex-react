using AutoMapper;
using FlexCore.Models.DTOs;
using FlexCore.Repositories.Interfaces;
using FlexCore.Services.Interfaces;

namespace FlexCore.Services.Implementations
{
	public class TopCategoryService : ITopCategoryService
	{
		private readonly ITopCategoryRepository _repo;
		private readonly IMapper _mapper;

		public TopCategoryService(ITopCategoryRepository topCategoryRepository, IMapper mapper)
		{
			_repo = topCategoryRepository;
			_mapper = mapper;
		}

		public async Task<IEnumerable<TopCategoryDto>> GetTopCategoriesAsync()
		{
			var topCategories = await _repo.GetTopCategoriesAsync();
			return _mapper.Map<IEnumerable<TopCategoryDto>>(topCategories);
		}
	}
}
