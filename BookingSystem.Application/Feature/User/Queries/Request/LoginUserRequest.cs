using BookingSystem.DTOs.Authentication;
using MediatR;

namespace BookingSystem.Application.Feature.User.Queries.Request
{
	public record LoginUserRequest(LoginRequest Login) : IRequest<AuthenticateResponse>;
}
