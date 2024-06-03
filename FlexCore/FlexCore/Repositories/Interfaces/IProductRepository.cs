using FlexCore.Models;
using FlexCore.Models.Entities;
using System.Xml.Serialization;

namespace FlexCore.Repositories.Interfaces
{
	public interface IProductRepository
	{
		Task<Page<ProductEntity>> GetProductsPageAsync(Pageable pageable,
			int? topCategoryId,
			int? middleCategoryId,
			int? bottomCategoryId,
			int? maxPrice, 
			int? minPrice);

		Task<ProductEntity> GetProductByIdAsync(string id);

	}
}
