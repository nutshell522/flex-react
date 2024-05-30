using FlexCore.Models.Entities;
using System.Xml.Serialization;

namespace FlexCore.Repositories.Interfaces
{
	public interface IProductRepository
	{
		Task<IEnumerable<ProductEntity>> GetProductsAsync();

	}
}
