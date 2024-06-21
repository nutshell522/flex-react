using AutoMapper;
using FlexCore.Models.DTOs;
using FlexCore.Repositories.Interfaces;
using FlexCore.Services.Interfaces;

namespace FlexCore.Services.Implementations
{
	public class MiddleCategoryService: IMiddleCategoryService
	{
		private readonly IMiddleCategoryRepository _repo;
		private readonly IMapper _mapper;

		public MiddleCategoryService(IMiddleCategoryRepository middleCategoryRepository, IMapper mapper)
		{
			_repo = middleCategoryRepository;
			_mapper = mapper;
		}

		public async Task<IEnumerable<MiddleCategoryDto>> GetMiddleCategoriesAsync(int TopCategoryId)
		{
			var middleCategories = await _repo.GetMiddleCategoriesAsync(TopCategoryId);
			return _mapper.Map<IEnumerable<MiddleCategoryDto>>(middleCategories);
		}

	}
}
