namespace FlexCore.Models.Entities
{
    public class TopCategory
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public ICollection<MiddleCategory>? MiddleCategories { get; set; }

    }
}
