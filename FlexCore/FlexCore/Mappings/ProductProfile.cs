using AutoMapper;
using FlexCore.Models.DTOs;
using FlexCore.Models.Entities;

namespace FlexCore.Mappings
{
    public class ProductProfile :Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductEntity,ProductDto>();
            CreateMap<ProductDto,ProductEntity>();
        }
    }
}
