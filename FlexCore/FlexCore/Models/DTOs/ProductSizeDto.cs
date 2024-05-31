namespace FlexCore.Models.DTOs
{
    public class ProductSizeDto
    {
        public int Id { get; set; }
        public int SizeOptionId { get; set; }
        public int ProductColorId { get; set; }
        public int Stock { get; set; }
        public SizeOptionDto? SizeOption { get; set; }
        public ProductColorDto? ProductColor { get; set; }
    }
}
