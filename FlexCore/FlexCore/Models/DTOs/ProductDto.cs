namespace FlexCore.Models.DTOs
{
    public class ProductDto
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? UnitPrice { get; set; }
        public int SalesPrice { get; set; }
        public bool? Status { get; set; }
        public int? BottomCategoryId { get; set; }
        public BottomCategoryDto? BottomCategory { get; set; }
        public ICollection<ProductColorDto>? ProductColors { get; set; }
    }
}
