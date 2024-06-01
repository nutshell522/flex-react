using AutoMapper;
using FlexCore.Models.DTOs;
using FlexCore.Models.Entities;
using FlexCore.Models.ViewModels.ClientViewModels.Product;
using FlexCore.Models.ViewModels.ClientViewModels.ProductColor;

namespace FlexCore.Mappings
{
    public class ProductProfile :Profile
    {
        public ProductProfile()
		{
			CreateMap<ProductEntity, ProductDto>();
			CreateMap<ProductDto, ProductEntity>();

			CreateMap<ProductDto, ProductListVM>()
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
					Name = productColor.ColorOption.Name,
					// Map other properties as needed
				});
			}
			return productColorVMs;
		}
	}
}
