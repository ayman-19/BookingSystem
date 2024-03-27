using MediatR;

namespace BookingSystem.Application.Feature.User.Commands.Request
{
	public record ResetPasswordRequest(string newPass, string code) : IRequest<string>;
}
