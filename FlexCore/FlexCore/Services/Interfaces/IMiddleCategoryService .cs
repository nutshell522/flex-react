using FlexCore.Models.DTOs;

namespace FlexCore.Services.Interfaces
{
	public interface IMiddleCategoryService
	{
		Task<IEnumerable<MiddleCategoryDto>> GetMiddleCategoriesAsync(int TopCategoryId);
	}
}
