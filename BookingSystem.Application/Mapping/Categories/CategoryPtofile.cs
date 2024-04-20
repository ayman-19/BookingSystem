using AutoMapper;
using BookingSystem.Domain.Model;
using BookingSystem.DTOs.Category;

namespace BookingSystem.Application.Mapping.Categories
{
    public class CategoryPtofile : Profile
    {

        public CategoryPtofile()
        {
            CreateMapForCategory();
        }

        private void CreateMapForCategory()
        {
            CreateMap<Category, CategoryQuery>().ReverseMap();
            CreateMap<Category, CategoryCommand>().ReverseMap();
        }
    }
}
