using FlexCore.Models.DTOs;

namespace FlexCore.Services.Interfaces
{
	public interface ITopCategoryService
	{
		Task<IEnumerable<TopCategoryDto>> GetTopCategoriesAsync();
	}
}
