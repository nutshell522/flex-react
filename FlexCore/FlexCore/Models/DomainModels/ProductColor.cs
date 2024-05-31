namespace FlexCore.Models.DomainModels
{
    public class ProductColor
    {
        public int Id { get; set; }
        public string? ProductId { get; set; }
        public int? ColorOptionId { get; set; }
        public Product? Product { get; set; }
        public ColorOption? ColorOption { get; set; }
        public ICollection<ProductSize>? ProductSize { get; set; }
        public ICollection<ProductPicture>? ProductPictures { get; set; }
    }
}
