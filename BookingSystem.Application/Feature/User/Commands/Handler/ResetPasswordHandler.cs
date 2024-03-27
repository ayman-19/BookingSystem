using BookingSystem.Application.Feature.User.Commands.Request;
using BookingSystem.Application.IAuthentication;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BookingSystem.Application.Feature.User.Commands.Handler
{
	public class ResetPasswordHandler : IRequestHandler<ResetPasswordRequest, string>
	{
		private readonly IAccount _account;
		private readonly IHttpContextAccessor _httpContext;

		public ResetPasswordHandler(IAccount account, IHttpContextAccessor httpContext)
		{
			_account = account;
			_httpContext = httpContext;
		}

		public async Task<string> Handle(ResetPasswordRequest request, CancellationToken cancellationToken)
		=> await _account.ResetPasswordAsync(
			_httpContext.HttpContext.User.Claims.First(u => u.Type.Equals(ClaimTypes.Email)).Value
			, request.newPass, request.code);
	}
}
