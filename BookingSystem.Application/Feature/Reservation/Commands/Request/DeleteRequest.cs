using BookingSystem.DTOs.Reservation;
using MediatR;

namespace BookingSystem.Application.Feature.Reservation.Commands.Request
{
    public record DeleteRequest(int id) : IRequest<ReservationQuery>;
}
