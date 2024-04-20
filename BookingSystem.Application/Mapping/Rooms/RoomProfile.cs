using AutoMapper;
using BookingSystem.Domain.Model;
using BookingSystem.DTOs.Room;

namespace BookingSystem.Application.Mapping.Rooms
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            Map();
        }
        private void Map()
        {
            CreateMap<Room, RoomQuery>().ForMember(p => p.Floor, mce => mce.MapFrom(src => src.Floor.Number)).ReverseMap();
            CreateMap<Room, RoomCommand>().ReverseMap();
        }
    }
}
