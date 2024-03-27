using BookingSystem.Application.IAuthentication;
using BookingSystem.DTOs.Authentication;
using MediatR;

namespace BookingSystem.Application.Feature.User.Commands.Handler
{
	public class RegisterHandler : IRequestHandler<Request.RegisterRequest, AuthenticateResponse>
	{
		private readonly IAccount _account;

		public RegisterHandler(IAccount account)
		{
			_account = account;
		}

		public async Task<AuthenticateResponse> Handle(Request.RegisterRequest request, CancellationToken cancellationToken)
		=> await _account.RegisterAsync(request.register);

	}
}
