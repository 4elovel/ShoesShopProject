using AutoMapper;
using ShoesShopProject.Models;
using ShoesShopProject.ViewModels;

namespace ShoesShopProject.Infrastructure;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<Good, GoodCardViewModel>().ReverseMap();
        CreateMap<Good, GoodDetailsViewMidel>().ReverseMap();
        CreateMap<Good, GoodOrderViewModel>().ReverseMap();
    }
}
