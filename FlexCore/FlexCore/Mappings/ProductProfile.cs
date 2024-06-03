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
			// Mapping for BottomCategoryEntity to BottomCategoryDto
			CreateMap<BottomCategoryEntity, BottomCategoryDto>();

			// Mapping for ProductColorEntity to ProductColorDto
			CreateMap<ProductColorEntity, ProductColorDto>();

			// Mapping for ProductEntity to ProductDto
			CreateMap<ProductEntity, ProductDto>()
				.ForMember(dest => dest.BottomCategory, opt => opt.MapFrom(src => src.BottomCategory != null ? src.BottomCategory : null))
				.ForMember(dest => dest.ProductColors, opt => opt.MapFrom(src => src.ProductColors != null ? src.ProductColors : new List<ProductColorEntity>()));

			// Reverse mapping for ProductDto to ProductEntity
			CreateMap<ProductDto, ProductEntity>()
				.ForMember(dest => dest.BottomCategory, opt => opt.MapFrom(src => src.BottomCategory != null ? src.BottomCategory : null))
				.ForMember(dest => dest.ProductColors, opt => opt.MapFrom(src => src.ProductColors != null ? src.ProductColors : new List<ProductColorDto>()));

			// Mapping for ProductDto to ProductListVM
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
