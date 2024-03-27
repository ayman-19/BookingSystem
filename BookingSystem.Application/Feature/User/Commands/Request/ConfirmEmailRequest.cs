using MediatR;

namespace BookingSystem.Application.Feature.User.Commands.Request
{
	public record ConfirmEmailRequest(string userId, string token) : IRequest<string>;
}
