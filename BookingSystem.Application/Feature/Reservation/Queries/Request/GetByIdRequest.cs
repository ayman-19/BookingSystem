using BookingSystem.DTOs.Reservation;
using MediatR;

namespace BookingSystem.Application.Feature.Reservation.Queries.Request
{
    public record GetByIdRequest(int id) : IRequest<ReservationQuery>;
}
