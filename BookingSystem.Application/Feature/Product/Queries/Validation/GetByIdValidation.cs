using BookingSystem.Application.Feature.Product.Queries.Request;
using BookingSystem.Application.IRepositories;
using FluentValidation;

namespace BookingSystem.Application.Feature.Product.Queries.Validation
{
    public class GetByIdValidation : AbstractValidator<GetByIdRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetByIdValidation(IUnitOfWork unitOfWork)
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
        }
    }
}
