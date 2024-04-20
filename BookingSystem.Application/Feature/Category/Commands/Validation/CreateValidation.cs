using BookingSystem.Application.Feature.Category.Commands.Request;
using BookingSystem.Application.IRepositories;
using FluentValidation;

namespace BookingSystem.Application.Feature.Category.Commands.Validation
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
            RuleFor(req => req.Command.Name)
                .NotEmpty().WithMessage("Name Not Empty")
                .NotNull().WithMessage("Name Not Null")
                .MustAsync(async (name, CancellationToken) => !await _unitOfWork.Categories.IsAnyExistAsync(r => r.Name == name)).WithMessage("Name Is Exist");

            RuleFor(req => req.Command.Description)
                .NotEmpty().WithMessage("Description Not Empty")
                .NotNull().WithMessage("Description Not Null");
        }
    }
}
