namespace FlexCore.Models.DTOs
{
    public class ProductColorDto
    {
        public int Id { get; set; }
        public string? ProductId { get; set; }
        public int? ColorOptionId { get; set; }
        public ColorOptionDto? ColorOption { get; set; }
        public ICollection<ProductSizeDto>? ProductSize { get; set; }
        public ICollection<ProductPictureDto>? ProductPictures { get; set; }
    }
}
