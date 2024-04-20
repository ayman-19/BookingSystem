using BookingSystem.Application.Feature.Category.Commands.Request;
using BookingSystem.Application.IRepositories;
using FluentValidation;

namespace BookingSystem.Application.Feature.Category.Commands.Validation
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
                .MustAsync(async (id, CancellationToken) => await _unitOfWork.Categories.IsAnyExistAsync(r => r.Id == id)).WithMessage("Category Is Not Exist");

            RuleFor(req => req.Command.Name)
                .NotEmpty().WithMessage("Name Not Empty")
                .NotNull().WithMessage("Name Not Null");

            RuleFor(req => req)
               .MustAsync(async (command, CancellationToken) => !await _unitOfWork.Categories.IsAnyExistAsync(r => r.Name == command.Command.Name && r.Id == command.id)).WithMessage("Name Is Exist");

            RuleFor(req => req.Command.Description)
                .NotEmpty().WithMessage("Description Not Empty")
                .NotNull().WithMessage("Description Not Null");
        }
    }
}
