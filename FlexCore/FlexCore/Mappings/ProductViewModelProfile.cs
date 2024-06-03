using AutoMapper;
using FlexCore.Models.DTOs;
using FlexCore.Models.ViewModels.ClientViewModels.Product;
using FlexCore.Models.ViewModels.ClientViewModels.ProductColor;

namespace FlexCore.Mappings
{
    public class ProductViewModelProfile:Profile
    {
        public ProductViewModelProfile()
        {
            CreateMap<ProductDto,ProductListVM>()
                .ForMember(dest => dest.ProductColors, opt => opt.MapFrom(src => MapProductColors(src.ProductColors)));
        }

        private List<ProductColorVM> MapProductColors(ICollection<ProductColorDto> productColors)
        {
            var productColorVMs = new List<ProductColorVM>();
            foreach (var productColor in productColors)
            {
                productColorVMs.Add(new ProductColorVM
                {
                    Id = productColor.Id,
                    Color = productColor.ColorOption.Color,
                    Name = productColor.ColorOption.Name,
                    // Map other properties as needed
                });
            }
            return productColorVMs;
        }
    }
}
