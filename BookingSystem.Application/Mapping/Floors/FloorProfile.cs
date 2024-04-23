using AutoMapper;
using BookingSystem.Domain.Model;
using BookingSystem.DTOs.Floors;

namespace BookingSystem.Application.Mapping.Floors
{
    public class FloorProfile : Profile
    {
        public FloorProfile()
        {
            Map();
        }
        private void Map()
        {
            CreateMap<Floor, FloorQuery>().ReverseMap();
            CreateMap<Floor, FloorCommand>().ReverseMap();
            CreateMap<Floor, FloorDetails>().ForMember(des => des.Rooms, ops => ops.MapFrom(src => src.Rooms));
        }
    }
}
