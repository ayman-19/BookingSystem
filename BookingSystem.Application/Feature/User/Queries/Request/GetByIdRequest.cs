using BookingSystem.DTOs.Authentication;
using MediatR;

namespace BookingSystem.Application.Feature.User.Queries.Request
{
    public record GetByIdRequest(string id) : IRequest<UserReservedRespose>;
}
