using BookingSystem.Application.Feature.User.Queries.Request;
using BookingSystem.Application.IAuthentication;
using BookingSystem.DTOs.Authentication;
using MediatR;

namespace BookingSystem.Application.Feature.User.Queries.Handler
{
	public class LoginUserHandler : IRequestHandler<LoginUserRequest, AuthenticateResponse>
	{
		private readonly IAccount _account;

		public LoginUserHandler(IAccount account)
		{
			_account = account;
		}
		public async Task<AuthenticateResponse> Handle(LoginUserRequest request, CancellationToken cancellationToken)
		=> await _account.LoginAsync(request.Login);
	}
}
