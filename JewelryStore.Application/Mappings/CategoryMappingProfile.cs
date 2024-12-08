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
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Category, CategoryDto>()
                    .ForMember(dest => dest.SubCategories, opt => opt.MapFrom(src => src.SubCategories));
        }
    }
}
