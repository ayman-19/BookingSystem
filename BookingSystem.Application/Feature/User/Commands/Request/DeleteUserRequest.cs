using MediatR;

namespace BookingSystem.Application.Feature.User.Commands.Request
{
	public record DeleteUserRequest(string userId) : IRequest;
}
