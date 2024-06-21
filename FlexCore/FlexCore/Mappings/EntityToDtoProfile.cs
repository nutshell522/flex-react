using AutoMapper;
using FlexCore.Models.DTOs;
using FlexCore.Models.Entities;
using FlexCore.Models.ViewModels.ClientViewModels.Product;
using FlexCore.Models.ViewModels.ClientViewModels.ProductColor;

namespace FlexCore.Mappings
{
    public class EntityToDtoProfile :Profile
    {
		public EntityToDtoProfile()
		{
            // Product Models Mapping
            CreateMap<ColorOptionEntity, ColorOptionDto>().ReverseMap();
            CreateMap<SizeOptionEntity, SizeOptionDto>().ReverseMap();
            CreateMap<TopCategoryEntity, TopCategoryDto>().ReverseMap();
            CreateMap<MiddleCategoryEntity, MiddleCategoryDto>().ReverseMap();
            CreateMap<BottomCategoryEntity, BottomCategoryDto>().ReverseMap();
            CreateMap<ProductEntity, ProductDto>().ReverseMap();
            CreateMap<ProductColorEntity, ProductColorDto>().ReverseMap();
            CreateMap<ProductSizeEntity, ProductSizeDto>().ReverseMap();
            CreateMap<ProductPictureEntity, ProductPictureDto>().ReverseMap();
            CreateMap<UserEntity, UserDto>().ReverseMap();
        }
	}
}
