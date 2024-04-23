using BookingSystem.Application.Feature.Foods.Commands.Request;
using BookingSystem.Application.IRepositories;
using FluentValidation;

namespace BookingSystem.Application.Feature.Foods.Commands.Validation
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
            RuleFor(req => req.Command.Amount)
                .NotEmpty().WithMessage("Amount Not Empty")
                .NotNull().WithMessage("Amount Not Null")
                .GreaterThan(0).WithMessage("Enter Postive Amount");

            RuleFor(req => req.Command.ProductId)
                .NotEmpty().WithMessage("Product Id Not Empty")
                .NotNull().WithMessage("Product Id Not Null")
                .MustAsync(async (id, CancellationToken) => await _unitOfWork.Products.IsAnyExistAsync(i => i.Id == id)).WithMessage("Product Is Not Exist!");

            RuleFor(req => req.Command)
                .MustAsync(async (req, CancellationToken) => await _unitOfWork.Products.IsAnyExistAsync(i => i.Id == req.ProductId && i.Quantity >= req.Amount)).WithMessage("Enter Amount Is Valid!");
        }
    }
}
