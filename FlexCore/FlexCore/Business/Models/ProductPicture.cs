namespace FlexCore.Business.Models
{
    public class ProductPicture
    {
        public int Id { get; set; }
        public int ProductColorId { get; set; }
        public string? Url{ get; set; }
        public ProductColor? ProductColor { get; set; }
    }
}
