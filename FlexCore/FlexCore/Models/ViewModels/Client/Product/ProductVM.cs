using FlexCore.Models.ViewModels.ClientViewModels.ProductColor;

namespace FlexCore.Models.ViewModels.Client.Product
{
	public class ProductVM
	{
		public string? Id { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
		public int? UnitPrice { get; set; }
		public int SalesPrice { get; set; }
		public ICollection<ProductColorVM>? ProductColors { get; set; }
	}
}
