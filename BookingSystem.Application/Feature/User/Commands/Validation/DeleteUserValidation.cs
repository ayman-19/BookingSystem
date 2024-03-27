using BookingSystem.Application.Feature.User.Commands.Request;
using BookingSystem.Application.IRepositories;
using FluentValidation;

namespace BookingSystem.Application.Feature.User.Commands.Validation
{
	public class DeleteUserValidation : AbstractValidator<DeleteUserRequest>
	{
		private readonly IUnitOfWork _uW;

		public DeleteUserValidation(IUnitOfWork uW)
		{
			_uW = uW;
			CustomValidationForUser();
		}

		private void CustomValidationForUser()
		{
			RuleFor(user => user.userId)
				.NotEmpty().WithMessage("User Id Not Empty")
				.NotNull().WithMessage("User Id Not Null")
				.MustAsync(async (id, CancellationToken) => await _uW.Users.IsAnyExistAsync(user => user.Id == id)).WithMessage("User Not Fount");
		}
	}
}
