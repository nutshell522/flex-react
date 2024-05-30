namespace FlexCore.Models.Entities
{
    public class ProductColorEntity
    {
        public int Id { get; set; }
        public string? ProductId { get; set; }
        public int? ColorOptionId { get; set; }
        public ProductEntity? Product { get; set; }
        public ColorOptionEntity? ColorOption { get; set; }
        public ICollection<ProductSizeEntity>? ProductSize { get; set; }
        public ICollection<ProductPictureEntity>? productPictures { get; set; }
    }
}
