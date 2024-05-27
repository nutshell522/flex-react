namespace FlexCore.Business.Models
{
    public class MiddleCategory
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
		public string? Code { get; set; }
		public int? TopCategoryId { get; set; } 
        public TopCategory? TopCategory { get; set; }
        public ICollection<BottomCategory>? BottomCategories { get; set; }
    }
}
