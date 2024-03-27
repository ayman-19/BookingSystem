using BookingSystem.Application.Feature.User.Commands.Request;
using BookingSystem.Application.IRepositories;
using FluentValidation;

namespace BookingSystem.Application.Feature.User.Commands.Validation
{
	public class RegisterValidate : AbstractValidator<RegisterRequest>
	{
		private readonly IUnitOfWork _uW;

		public RegisterValidate(IUnitOfWork uW)
		{
			_uW = uW;
			CustomValidationForUser();
		}

		private void CustomValidationForUser()
		{
			RuleFor(u => u.register.UsarName)
				.NotEmpty().WithMessage("User Name Not Empty")
				.NotNull().WithMessage("User Name Not Null")
				.MustAsync(async (userName, CancellationToken) => !await _uW.Users.IsAnyExistAsync(u => u.UserName!.Equals(userName))).WithMessage("User Name Is Exist");

			RuleFor(u => u.register.Email)
				.NotEmpty().WithMessage("Email Not Empty")
				.NotNull().WithMessage("Email Not Null")
				.MustAsync(async (email, CancellationToken) => !await _uW.Users.IsAnyExistAsync(u => u.Email!.Equals(email))).WithMessage("Email Is Exist");

			RuleFor(u => u.register.Password)
				.NotEmpty().WithMessage("Password Not Empty")
				.NotNull().WithMessage("Password Not Null");

			RuleFor(u => u.register.ConfirmPassword)
				.NotEmpty().WithMessage("Confirm Password Not Empty")
				.NotNull().WithMessage("Confirm Password Not Null");

			RuleFor(u => u.register.Password)
				.Equal(ps => ps.register.ConfirmPassword)
				.WithMessage("Password and Confirm Password Not Equal");

			RuleFor(u => u.register.Phone)
				.NotEmpty().WithMessage("Phone Not Empty")
				.NotNull().WithMessage("Phone Not Null");

			RuleFor(u => u.register.Name)
				.NotEmpty().WithMessage("Name Not Empty")
				.NotNull().WithMessage("Name Not Null");

			RuleFor(u => u.register.Address)
				.NotEmpty().WithMessage("Address Not Empty")
				.NotNull().WithMessage("Address Not Null");

			RuleFor(u => u.register.BirthDate)
				.NotEmpty().WithMessage("BirthDate Not Empty")
				.NotNull().WithMessage("BirthDate Not Null");
		}
	}
}
