namespace FlexCore.Models.Entities
{
    public class MiddleCategoryEntity
	{
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public int? TopCategoryId { get; set; }
        public TopCategoryEntity? TopCategory { get; set; }
        public ICollection<BottomCategoryEntity>? BottomCategories { get; set; }
    }
}
