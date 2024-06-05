using System.Text.Json.Serialization;

namespace FlexCore.Models.Entities
{
    public class TopCategoryEntity
	{
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public virtual ICollection<MiddleCategoryEntity>? MiddleCategories { get; set; }

    }
}
