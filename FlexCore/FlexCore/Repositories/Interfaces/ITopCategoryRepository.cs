using FlexCore.Models.Entities;

namespace FlexCore.Repositories.Interfaces
{
	public interface ITopCategoryRepository
	{
		Task<IEnumerable<TopCategoryEntity>> GetTopCategoriesAsync();
	}
}
