using BookingSystem.Application.Feature.Reservation.Commands.Request;
using BookingSystem.Application.IRepositories;
using FluentValidation;

namespace BookingSystem.Application.Feature.Reservation.Commands.Validation
{
    internal class UpdateValidation : AbstractValidator<UpdateRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateValidation(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CustomValidation();
        }

        private void CustomValidation()
        {
            RuleFor(re => re.Command.Date)
                .NotEmpty().WithMessage("Date Not Empty")
                .NotNull().WithMessage("Date Not Null");

            RuleFor(re => re)
                .MustAsync(async (req, CancellationToken) => !await _unitOfWork.Reservations.IsAnyExistAsync(re => re.Date.Date == req.Command.Date.Date && re.Id != req.id))
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
