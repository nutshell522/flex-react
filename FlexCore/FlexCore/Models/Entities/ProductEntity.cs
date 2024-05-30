namespace FlexCore.Models.Entities
{
    public class ProductEntity
	{
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? UnitPrice { get; set; }
        public int SalesPrice { get; set; }
        public bool? Status { get; set; }
        public int? BottomCategoryId { get; set; }
        public BottomCategoryEntity? BottomCategory { get; set; }
        public ICollection<ProductColorEntity>? ProductColors { get; set; }
    }
}
