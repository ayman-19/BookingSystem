using BookingSystem.Application.Feature.User.Queries.Request;
using BookingSystem.Application.IAuthentication;
using MediatR;

namespace BookingSystem.Application.Feature.User.Queries.Handler
{
	public class SendResetPasswordCodeHandler : IRequestHandler<SendResetPasswordCodeRequest, string>
	{
		private readonly IAccount _account;

		public SendResetPasswordCodeHandler(IAccount account)
		{
			_account = account;
		}
		public Task<string> Handle(SendResetPasswordCodeRequest request, CancellationToken cancellationToken)
		=> _account.SendResetPasswordCodeAsync(request.email);
	}
}
