using BookingSystem.Application.Feature.Rooms.Commands.Request;
using BookingSystem.Application.IRepositories;
using FluentValidation;

namespace BookingSystem.Application.Feature.Rooms.Commands.Validation
{
    public class UpdateValidation : AbstractValidator<UpdateRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateValidation(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CustomValidation();
        }
        private void CustomValidation()
        {
            RuleFor(r => r.id)
               .NotEmpty().WithMessage("Id Not Empty")
                .NotNull().WithMessage("Id Not Null")
                .MustAsync(async (id, CancellationToken) => await _unitOfWork.Rooms.IsAnyExistAsync(r => r.Id == id)).WithMessage("Room Is Not Exist");

            RuleFor(r => r.Command.Code)
                .NotEmpty().WithMessage("Code Not Empty")
                .NotNull().WithMessage("Code Not Null");

            RuleFor(r => r.Command.Floor)
                .NotEmpty().WithMessage("Floor Not Empty")
                .NotNull().WithMessage("Floor Not Null");

            RuleFor(r => r)
              .MustAsync(async (req, CancellationToken) => !await _unitOfWork.Rooms.IsAnyExistAsync(r => r.Code == req.Command.Code && r.Id != req.id)).WithMessage("Code Is Exist");
        }
    }
}
