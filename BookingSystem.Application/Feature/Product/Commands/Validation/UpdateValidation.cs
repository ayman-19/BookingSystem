using BookingSystem.Application.Feature.Product.Commands.Request;
using BookingSystem.Application.IRepositories;
using FluentValidation;

namespace BookingSystem.Application.Feature.Product.Commands.Validation
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
                .MustAsync(async (id, CancellationToken) => await _unitOfWork.Products.IsAnyExistAsync(r => r.Id == id)).WithMessage("Product Is Not Exist");

            RuleFor(req => req.Command.Name)
                .NotEmpty().WithMessage("Name Not Empty")
                .NotNull().WithMessage("Name Not Null");

            RuleFor(req => req.Command.Quantity)
            .NotEmpty().WithMessage("Quantity Not Empty")
            .NotNull().WithMessage("Quantity Not Null")
            .GreaterThan(0).WithMessage("Quantity Not Nigetive");

            RuleFor(req => req)
               .MustAsync(async (command, CancellationToken) => !await _unitOfWork.Categories.IsAnyExistAsync(r => r.Name == command.Command.Name && r.Id == command.id)).WithMessage("Name Is Exist");

            RuleFor(req => req.Command.CategoryId)
                .NotEmpty().WithMessage("Category Not Empty")
                .NotNull().WithMessage("Category Not Null")
                .MustAsync(async (id, CancellationToken) => await _unitOfWork.Categories.IsAnyExistAsync(r => r.Id == id)).WithMessage("Category Is Not Exist");
        }
    }
}
