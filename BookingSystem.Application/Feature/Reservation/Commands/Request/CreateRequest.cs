using BookingSystem.DTOs.Reservation;
using MediatR;

namespace BookingSystem.Application.Feature.Reservation.Commands.Request
{
    public record CreateRequest(ReservationCommand Command) : IRequest<ReservationQuery>;
}
