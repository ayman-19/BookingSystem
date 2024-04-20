using BookingSystem.Application.Feature.Rooms.Commands.Request;
using BookingSystem.Application.IRepositories;
using FluentValidation;

namespace BookingSystem.Application.Feature.Rooms.Commands.Validation
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
            RuleFor(r => r.Command.Code)
                .NotEmpty().WithMessage("Code Not Empty")
                .NotNull().WithMessage("Code Not Null")
                .MustAsync(async (code, CancellationToken) => !await _unitOfWork.Rooms.IsAnyExistAsync(r => r.Code == code)).WithMessage("Code Is Exist");

            RuleFor(r => r.Command.FloorId)
                .NotEmpty().WithMessage("Floor Not Empty")
                .NotNull().WithMessage("Floor Not Null")
                 .MustAsync(async (id, CancellationToken) => await _unitOfWork.Floors.IsAnyExistAsync(r => r.Id == id)).WithMessage("Floor Is Not Exist");
        }
    }
}
