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
            CreateMap<Room, RoomQuery>().ReverseMap();
            CreateMap<Room, RoomCommand>().ReverseMap();
        }
    }
}
