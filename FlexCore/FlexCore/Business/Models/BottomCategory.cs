namespace FlexCore.Business.Models
{
    public class BottomCategory
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public int? MiddleCategoryId { get; set; }
        public MiddleCategory? MiddleCategory { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
