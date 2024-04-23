using BookingSystem.Application.Feature.Floors.Commnds.Request;
using BookingSystem.Application.IRepositories;
using FluentValidation;

namespace BookingSystem.Application.Feature.Floors.Commnds.Validation
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
                .MustAsync(async (id, CancellationToken) => await _unitOfWork.Floors.IsAnyExistAsync(r => r.Id == id)).WithMessage("Floor Is Not Exist");

            RuleFor(req => req.Command.Number)
                .NotEmpty().WithMessage("Name Not Empty")
                .NotNull().WithMessage("Name Not Null");

            RuleFor(req => req)
               .MustAsync(async (command, CancellationToken) => !await _unitOfWork.Floors.IsAnyExistAsync(r => r.Number == command.Command.Number && r.Id == command.id)).WithMessage("Number Is Exist");

        }
    }
}
