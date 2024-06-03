using System.Text.Json.Serialization;

namespace FlexCore.Models.Entities
{
    public class ProductSizeEntity
	{
        public int Id { get; set; }
        public int SizeOptionId { get; set; }
        public int ProductColorId { get; set; }
        public int Stock { get; set; }
        public SizeOptionEntity? SizeOption { get; set; }
        [JsonIgnore]
        public ProductColorEntity? ProductColor { get; set; }
    }
}
