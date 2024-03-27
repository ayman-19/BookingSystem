using MediatR;

namespace BookingSystem.Application.Feature.User.Queries.Request
{
	public record SendResetPasswordCodeRequest(string email) : IRequest<string>;
}
