using AutoMapper;
using JewelryStore.Application.DTOs;
using JewelryStore.Web.ViewModels;

namespace JewelryStore.Web.Mappings
{
    public class ViewModelMappingProfile : Profile
    {
        public ViewModelMappingProfile()
        {
            CreateMap<ProductDto, ProductViewModel>();
            CreateMap<ProductViewModel, ProductDto>();
        }
    }
}
