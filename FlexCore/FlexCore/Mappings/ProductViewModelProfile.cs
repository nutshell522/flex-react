using AutoMapper;
using FlexCore.Models.DTOs;
using FlexCore.Models.ViewModels.Client.BottomCategory;
using FlexCore.Models.ViewModels.Client.MiddleCategory;
using FlexCore.Models.ViewModels.Client.Picture;
using FlexCore.Models.ViewModels.Client.Product;
using FlexCore.Models.ViewModels.Client.ProductSize;
using FlexCore.Models.ViewModels.Client.TopCategory;
using FlexCore.Models.ViewModels.ClientViewModels.Product;
using FlexCore.Models.ViewModels.ClientViewModels.ProductColor;

namespace FlexCore.Mappings
{
    public class ProductViewModelProfile:Profile
    {
        public ProductViewModelProfile()
        {
            CreateMap<TopCategoryDto, TopCategoryVM>();
            CreateMap<MiddleCategoryDto, MiddleCategoryVM>();
            CreateMap<BottomCategoryDto, BottomCategoryVM>();

            CreateMap<ProductDto,ProductListVM>()
                .ForMember(dest => dest.ProductColors, opt => opt.MapFrom(src => MapProductColors(src.ProductColors)));

			CreateMap<ProductDto, ProductVM>()
		        .ForMember(dest => dest.ProductColors, opt => opt.MapFrom(src => src.ProductColors));

			CreateMap<ProductColorDto, ProductColorVM>()
		        .ForMember(dest => dest.ProductSizes, opt => opt.MapFrom(src => src.ProductSizes))
		        .ForMember(dest => dest.ProductPictures, opt => opt.MapFrom(src => src.ProductPictures))
		        .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.ColorOption.Name));

			CreateMap<ProductSizeDto, ProductSizeVM>()
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.SizeOption.Name));

			CreateMap<ProductPictureDto, PictureVM>(); 
		}

        private List<ProductColorIndexVM> MapProductColors(ICollection<ProductColorDto> productColors)
        {
            var productColorVMs = new List<ProductColorIndexVM>();
            foreach (var productColor in productColors)
            {
                productColorVMs.Add(new ProductColorIndexVM
				{
                    Id = productColor.Id,
                    Color = productColor.ColorOption.Color,
                    Name = productColor.ColorOption.Name,
                    ProductPictures = productColor.ProductPictures.Select(p => new PictureVM
					{
						Url = p.Url
					}).ToList()
                    // Map other properties as needed
                });
            }
            return productColorVMs;
        }
    }
}
