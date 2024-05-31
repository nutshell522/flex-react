namespace FlexCore.Models.DTOs
{
    public class TopCategoryDto
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public ICollection<MiddleCategoryDto>? MiddleCategories { get; set; }

    }
}
