using AutoMapper;
using JewelryStore.Application.DTOs;
using JewelryStore.Web.Models;

namespace JewelryStore.Web.Mappings
{
    public class ViewModelMappingProfile : Profile
    {
        public ViewModelMappingProfile()
        {
            CreateMap<ProductDto, ProductViewModel>()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));
        }
    }
}
