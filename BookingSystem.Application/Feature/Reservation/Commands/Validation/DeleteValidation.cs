using BookingSystem.Application.Feature.Reservation.Commands.Request;
using BookingSystem.Application.IRepositories;
using FluentValidation;

namespace BookingSystem.Application.Feature.Reservation.Commands.Validation
{
    public class DeleteValidation : AbstractValidator<DeleteRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteValidation(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CustomValidation();
        }

        private void CustomValidation()
        {
            RuleFor(re => re.id)
                .NotEmpty().WithMessage("id Not Empty")
                .NotNull().WithMessage("id Not Null")
                .MustAsync(async (id, CancellationToken) => await _unitOfWork.Reservations.IsAnyExistAsync(re => re.Id == id))
                .WithMessage("Reservation Is Not Exist");
        }
    }
}
