using BookingSystem.Application.Feature.User.Queries.Request;
using BookingSystem.Application.IRepositories;
using FluentValidation;

namespace BookingSystem.Application.Feature.User.Queries.Validation
{
    public class GetByIdValidation : AbstractValidator<GetByIdRequest>
    {
        private readonly IUnitOfWork _uW;

        public GetByIdValidation(IUnitOfWork uW)
        {
            _uW = uW;
            CustomValidationForUser();
        }

        private void CustomValidationForUser()
        {
            RuleFor(user => user.id)
                .NotEmpty().WithMessage("User Id Not Empty")
                .NotNull().WithMessage("User Id Not Null")
                .MustAsync(async (id, CancellationToken) => await _uW.Users.IsAnyExistAsync(user => user.Id == id)).WithMessage("User Not Fount");
        }
    }
}
