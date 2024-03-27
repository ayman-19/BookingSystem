using MediatR;

namespace BookingSystem.Application.Feature.User.Commands.Request
{
	public record RevokeTokenRequest(string token) : IRequest<bool>;
}
