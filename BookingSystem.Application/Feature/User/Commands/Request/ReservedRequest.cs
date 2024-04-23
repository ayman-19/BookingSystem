using BookingSystem.DTOs.Authentication;
using MediatR;

namespace BookingSystem.Application.Feature.User.Commands.Request
{
    public record ReservedRequest(int reservationId) : IRequest<UserReservedRespose>;
}
