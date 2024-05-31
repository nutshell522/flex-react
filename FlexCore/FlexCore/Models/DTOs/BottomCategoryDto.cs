namespace FlexCore.Models.DTOs
{
    public class BottomCategoryDto
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public MiddleCategoryDto? MiddleCategory { get; set; }
        public ICollection<ProductDto>? Products { get; set; }
    }
}
