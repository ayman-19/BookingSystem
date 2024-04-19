using BookingSystem.DTOs.Reservation;
using MediatR;

namespace BookingSystem.Application.Feature.Reservation.Commands.Request
{
    public record UpdateRequest(int id, ReservationCommand Command) : IRequest<ReservationQuery>;
}
