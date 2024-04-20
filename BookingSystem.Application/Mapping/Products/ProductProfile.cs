using AutoMapper;
using BookingSystem.Domain.Model;
using BookingSystem.DTOs.Product;

namespace BookingSystem.Application.Mapping.Products
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            Map();
        }
        private void Map()
        {
            CreateMap<Product, ProductQuery>().ForMember(p => p.Category, mce => mce.MapFrom(src => src.Category!.Name)).ReverseMap();
            CreateMap<Product, ProductCommand>().ReverseMap();
        }
    }
}
