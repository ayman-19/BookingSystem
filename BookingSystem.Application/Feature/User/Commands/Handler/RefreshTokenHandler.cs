using BookingSystem.Application.Feature.User.Commands.Request;
using BookingSystem.Application.IAuthentication;
using BookingSystem.DTOs.Authentication;
using MediatR;

namespace BookingSystem.Application.Feature.User.Commands.Handler
{
	public class RefreshTokenHandler : IRequestHandler<RefreshTokenRequest, AuthenticateResponse>
	{
		private readonly IAccount _account;

		public RefreshTokenHandler(IAccount account)
		{
			_account = account;
		}

		public async Task<AuthenticateResponse> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
		=> await _account.RefreshTokenAsync(request.refToken);

	}
}
