using BookingSystem.Application.Feature.User.Queries.Request;
using BookingSystem.Application.IRepositories;
using FluentValidation;

namespace BookingSystem.Application.Feature.User.Queries.Validation
{
	public class SendResetPasswordCodeValidation : AbstractValidator<SendResetPasswordCodeRequest>
	{
		private readonly IUnitOfWork _uW;

		public SendResetPasswordCodeValidation(IUnitOfWork uW)
		{
			_uW = uW;
			CustomValidationForUser();
		}

		private void CustomValidationForUser()
		{
			RuleFor(u => u.email)
				.NotEmpty().WithMessage("Email Not Empty")
				.NotNull().WithMessage("Email Not Null")
				.MustAsync(async (email, CancellationToken) => await _uW.Users.IsAnyExistAsync(u => u.Email!.Equals(email))).WithMessage("Email Is Not Exist");
		}
	}
}
