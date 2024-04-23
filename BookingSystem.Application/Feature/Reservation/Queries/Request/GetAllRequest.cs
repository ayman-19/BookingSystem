using BookingSystem.DTOs.Reservation;
using MediatR;

namespace BookingSystem.Application.Feature.Reservation.Queries.Request
{
    public record GetAllRequest() : IRequest<IQueryable<ReservationQuery>>;
}
