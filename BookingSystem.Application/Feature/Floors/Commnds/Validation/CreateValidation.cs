using BookingSystem.Application.Feature.Floors.Commnds.Request;
using BookingSystem.Application.IRepositories;
using FluentValidation;

namespace BookingSystem.Application.Feature.Floors.Commnds.Validation
{
    public class CreateValidation : AbstractValidator<CreateRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateValidation(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CustomValidation();
        }

        private void CustomValidation()
        {
            RuleFor(req => req.Command.Number)
                .NotEmpty().WithMessage("Number Not Empty")
                .NotNull().WithMessage("Number Not Null")
                .MustAsync(async (number, CancellationToken) => !await _unitOfWork.Floors.IsAnyExistAsync(r => r.Number == number)).WithMessage("Number Is Exist");
        }
    }
}
