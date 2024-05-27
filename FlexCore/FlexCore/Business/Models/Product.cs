namespace FlexCore.Business.Models
{
    public class Product
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? UnitPrice { get; set; }
        public int SalesPrice { get; set; }
        public bool? Status { get; set; }
        public int? BottomCategoryId { get; set; }
        public BottomCategory? BottomCategory { get; set; }
        public ICollection<ProductColor>? ProductColors { get; set; }
    }
}
