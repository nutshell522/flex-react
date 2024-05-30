namespace FlexCore.Models.Entities
{
    public class ProductPicture
    {
        public int Id { get; set; }
        public int ProductColorId { get; set; }
        public string? Url { get; set; }
        public ProductColorEntity? ProductColor { get; set; }
    }
}
