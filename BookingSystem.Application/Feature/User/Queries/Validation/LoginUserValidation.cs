using BookingSystem.Application.Feature.User.Queries.Request;
using BookingSystem.Application.IRepositories;
using FluentValidation;

namespace BookingSystem.Application.Feature.User.Queries.Validation
{
	public class LoginUserValidation : AbstractValidator<LoginUserRequest>
	{
		private readonly IUnitOfWork _uW;

		public LoginUserValidation(IUnitOfWork uW)
		{
			_uW = uW;
			CustomValidationForUser();
		}

		private void CustomValidationForUser()
		{
			RuleFor(u => u.Login.Email)
				.NotEmpty().WithMessage("Email Not Empty")
				.NotNull().WithMessage("Email Not Null")
				.MustAsync(async (email, CancellationToken) => await _uW.Users.IsAnyExistAsync(u => u.Email!.Equals(email))).WithMessage("Email Is Not Exist");

			RuleFor(u => u.Login.Password)
				.NotEmpty().WithMessage("Password Not Empty")
				.NotNull().WithMessage("Password Not Null");
		}
	}
}
