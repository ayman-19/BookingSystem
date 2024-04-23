using AutoMapper;
using BookingSystem.Domain.Model;
using BookingSystem.DTOs.Authentication;

namespace BookingSystem.Application.Mapping.Users
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            Map();
        }

        private void Map()
        {
            CreateMap<User, UserReservedRespose>().ForMember(des => des.Floor, op => op.MapFrom(src => src.Room!.Floor.Number)).ForMember(des => des.Date, op => op.MapFrom(src => src.Reservation))
                .ForMember(des => des.Room, op => op.MapFrom(src => src.Room!.Code));
        }
    }
}
