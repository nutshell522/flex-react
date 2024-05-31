using FlexCore.Models.ViewModels.ClientViewModels.ProductColor;

namespace FlexCore.Models.ViewModels.ClientViewModels.Product
{
    public class ProductListVM
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? UnitPrice { get; set; }
        public int SalesPrice { get; set; }
        public List<ProductColorVM>? ProductColors { get; set; }
    }
}
