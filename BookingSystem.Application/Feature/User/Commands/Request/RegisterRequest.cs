using BookingSystem.DTOs.Authentication;
using MediatR;

namespace BookingSystem.Application.Feature.User.Commands.Request
{
	public record RegisterRequest(DTOs.Authentication.RegisterRequest register) : IRequest<AuthenticateResponse>;
}
