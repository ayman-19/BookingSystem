using AutoMapper;
using BookingSystem.Domain.Model;
using BookingSystem.DTOs.Reservation;

namespace BookingSystem.Application.Mapping.Reservations
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            Map();
        }

        private void Map()
        {
            CreateMap<Reservation, ReservationCommand>().ReverseMap();
            CreateMap<Reservation, ReservationQuery>().ReverseMap();
        }
    }
}
