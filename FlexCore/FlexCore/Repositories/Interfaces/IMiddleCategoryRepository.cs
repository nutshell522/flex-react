using FlexCore.Models.Entities;

namespace FlexCore.Repositories.Interfaces
{
	public interface IMiddleCategoryRepository
	{
		Task<IEnumerable<MiddleCategoryEntity>> GetMiddleCategoriesAsync(int TopCategoryId);
	}
}
