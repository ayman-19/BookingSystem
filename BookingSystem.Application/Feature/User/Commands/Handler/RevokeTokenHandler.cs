using BookingSystem.Application.Feature.User.Commands.Request;
using BookingSystem.Application.IAuthentication;
using MediatR;

namespace BookingSystem.Application.Feature.User.Commands.Handler
{
	public class RevokeTokenHandler : IRequestHandler<RevokeTokenRequest, bool>
	{
		private readonly IAccount _account;

		public RevokeTokenHandler(IAccount account)
		{
			_account = account;
		}

		public async Task<bool> Handle(RevokeTokenRequest request, CancellationToken cancellationToken)
		=> await _account.RevokeTokenAsync(request.token);
	}
}
