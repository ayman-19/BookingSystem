using BookingSystem.Application.Feature.Reservation.Commands.Request;
using BookingSystem.Application.IRepositories;
using FluentValidation;

namespace BookingSystem.Application.Feature.Reservation.Commands.Validation
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
            RuleFor(re => re.Command.Date)
                .NotEmpty().WithMessage("Date Not Empty")
                .NotNull().WithMessage("Date Not Null")
                .MustAsync(async (date, CancellationToken) => !await _unitOfWork.Reservations.IsAnyExistAsync(re => re.Date.Date == date.Date))
                .WithMessage("Date Is Exist");

            RuleFor(re => re.Command.Description)
                .NotEmpty().WithMessage("Description Not Empty")
                .NotNull().WithMessage("Description Not Null");

            RuleFor(re => re.Command.CreateOn)
                .NotEmpty().WithMessage("CreateOn Not Empty")
                .NotNull().WithMessage("CreateOn Not Null");
        }
    }
}
