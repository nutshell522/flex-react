using FlexCore.Models.DTOs;
using FlexCore.Models.ViewModels.Client.Picture;
using FlexCore.Models.ViewModels.Client.ProductSize;

namespace FlexCore.Models.ViewModels.ClientViewModels.ProductColor
{
    public class ProductColorVM
    {
        public int Id { get; set; }
        public string? Color { get; set; }
        public string? Name { get; set; }
        public ICollection<ProductSizeVM>? ProductSizes { get; set; }
        public ICollection<PictureVM>? ProductPictures { get; set; }
    }
}
