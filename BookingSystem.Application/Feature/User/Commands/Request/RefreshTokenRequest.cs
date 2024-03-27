using BookingSystem.DTOs.Authentication;
using MediatR;

namespace BookingSystem.Application.Feature.User.Commands.Request
{
	public record RefreshTokenRequest(string refToken) : IRequest<AuthenticateResponse>;
}
