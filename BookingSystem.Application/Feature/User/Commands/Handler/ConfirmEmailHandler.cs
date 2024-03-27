using BookingSystem.Application.Feature.User.Commands.Request;
using BookingSystem.Application.IAuthentication;
using MediatR;

namespace BookingSystem.Application.Feature.User.Commands.Handler
{
	public class ConfirmEmailHandler : IRequestHandler<ConfirmEmailRequest, string>
	{
		private readonly IAccount _account;

		public ConfirmEmailHandler(IAccount account)
		{
			_account = account;
		}
		public async Task<string> Handle(ConfirmEmailRequest request, CancellationToken cancellationToken)
		=> await _account.ConfirmEmailAsync(request.userId, request.token);
	}
}
