using BookingSystem.Application.Feature.User.Commands.Request;
using FluentValidation;

namespace BookingSystem.Application.Feature.User.Commands.Validation
{
	public class ResetPasswordValidation : AbstractValidator<ResetPasswordRequest>
	{

		public ResetPasswordValidation()
		{
			CustomValidationForUser();
		}

		private void CustomValidationForUser()
		{
			RuleFor(re => re.newPass)
			.NotEmpty().WithMessage("New Password Not Empty")
			.NotNull().WithMessage("New Password Not Null");

			RuleFor(re => re.code)
			.NotEmpty().WithMessage("Code Not Empty")
			.NotNull().WithMessage("Code Not Null");
		}
	}
}
