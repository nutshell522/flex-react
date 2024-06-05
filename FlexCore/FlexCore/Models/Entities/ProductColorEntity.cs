using System.Text.Json.Serialization;

namespace FlexCore.Models.Entities
{
    public class ProductColorEntity
    {
        public int Id { get; set; }
        public string? ProductId { get; set; }
        public int? ColorOptionId { get; set; }
        [JsonIgnore]
        public ProductEntity? Product { get; set; }
        public ColorOptionEntity? ColorOption { get; set; }
        public ICollection<ProductSizeEntity>? ProductSizes { get; set; }
        public ICollection<ProductPictureEntity>? ProductPictures { get; set; }
    }
}
