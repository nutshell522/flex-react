namespace FlexCore.Models.Entities
{
    public class BottomCategory
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public MiddleCategory? MiddleCategory { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
