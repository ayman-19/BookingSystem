using BookingSystem.Application.Feature.User.Commands.Request;
using BookingSystem.Application.IRepositories;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BookingSystem.Application.Feature.User.Commands.Validation
{
	public class RevokeTokenValidation : AbstractValidator<RevokeTokenRequest>
	{
		private readonly IHttpContextAccessor _httpContext;
		private readonly IUnitOfWork _uW;

		public RevokeTokenValidation(IHttpContextAccessor httpContext, IUnitOfWork uW)
		{
			_httpContext = httpContext;
			_uW = uW;
			CustomValidationForUser();
		}

		private void CustomValidationForUser()
		{
			var userId = _httpContext.HttpContext.User.Claims.First(u => u.Type == ClaimTypes.PrimarySid).Value;
			RuleFor(re => re.token)
				.NotEmpty().WithMessage("Token Not Empty")
				.NotNull().WithMessage("Token Not Null")
					.MustAsync(async (token, CancellationToken)
				=> (await _uW.Users.GetAsync(user => user.Id == userId)).RefreshTokens.Any(reftoken => reftoken.RefreshJwtToken == _httpContext.HttpContext.Request.Cookies["RefreshToken"] && reftoken.IsValid)).WithMessage("Token Not Valid");
		}
	}
}
