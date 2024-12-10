using AutoMapper;
using JewelryStore.Application.DTOs;
using JewelryStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryStore.Application.Mappings
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));

            CreateMap<ProductDto, Product>();
        }
    }
}
