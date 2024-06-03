using System.Text.Json.Serialization;

namespace FlexCore.Models.Entities
{
    public class BottomCategoryEntity
	{
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public int? MiddleCategoryId { get; set; }
        public MiddleCategoryEntity? MiddleCategory { get; set; }

        [JsonIgnore]
        public ICollection<ProductEntity>? Products { get; set; }
    }
}
