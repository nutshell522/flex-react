namespace FlexCore.Models.DTOs
{
    public class ProductPictureDto
    {
        public int Id { get; set; }
        public int ProductColorId { get; set; }
        public string? Url { get; set; }
        public ProductColorDto? ProductColor { get; set; }
    }
}
