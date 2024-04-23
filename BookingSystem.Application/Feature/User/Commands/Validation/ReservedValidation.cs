using BookingSystem.Application.Feature.User.Commands.Request;
using BookingSystem.Application.IRepositories;
using FluentValidation;

namespace BookingSystem.Application.Feature.User.Commands.Validation
{
    public class ReservedValidation : AbstractValidator<ReservedRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        public ReservedValidation(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CustomValidation();
        }

        private void CustomValidation()
        {
            RuleFor(r => r.reservationId)
                .NotEmpty().WithMessage("Reservation Id Not Empty")
                .NotNull().WithMessage("Reservation Id Not Null")
                .MustAsync(async (id, CancellationToken) => await _unitOfWork.Reservations.IsAnyExistAsync(r => r.Id == id))
                .WithMessage("Reservation Not Found");
        }
    }
}
