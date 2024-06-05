namespace FlexCore.Models.DTOs
{
    public class MiddleCategoryDto
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public int? TopCategoryId { get; set; }
        public ICollection<BottomCategoryDto>? BottomCategories { get; set; }
    }
}
