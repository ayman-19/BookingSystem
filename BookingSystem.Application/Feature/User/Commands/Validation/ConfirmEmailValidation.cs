using BookingSystem.Application.Feature.User.Commands.Request;
using BookingSystem.Application.IRepositories;
using FluentValidation;

namespace BookingSystem.Application.Feature.User.Commands.Validation
{
	public class ConfirmEmailValidation : AbstractValidator<ConfirmEmailRequest>
	{
		private readonly IUnitOfWork _uW;

		public ConfirmEmailValidation(IUnitOfWork uW)
		{
			_uW = uW;
			CustomValidationForUser();
		}

		private async void CustomValidationForUser()
		{
			RuleFor(user => user.token)
				.NotEmpty().WithMessage("Token Not Empty")
				.NotNull().WithMessage("Token Not Null");

			RuleFor(user => user.userId)
				.NotEmpty().WithMessage("User Id Not Empty")
				.NotNull().WithMessage("User Id Not Null")
				.MustAsync(async (id, CancellationToken) => await _uW.Users.IsAnyExistAsync(user => user.Id == id)).WithMessage("User Not Found");
		}
	}
}
