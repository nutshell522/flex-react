using FlexCore.Models.ViewModels.Client.Picture;

namespace FlexCore.Models.ViewModels.ClientViewModels.ProductColor
{
    public class ProductColorIndexVM
    {
        public int Id { get; set; }
        public string? Color { get; set; }
        public string? Name { get; set; }
        public ICollection<PictureVM> ProductPictures { get; set; }
    }
}
