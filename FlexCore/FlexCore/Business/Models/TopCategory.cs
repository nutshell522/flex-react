namespace FlexCore.Business.Models
{
    public class TopCategory
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public ICollection<MiddleCategory>? MiddleCategories { get; set; }

    }
}
