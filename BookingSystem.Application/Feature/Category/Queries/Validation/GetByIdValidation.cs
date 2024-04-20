using BookingSystem.Application.Feature.Category.Queries.Request;
using BookingSystem.Application.IRepositories;
using FluentValidation;

namespace BookingSystem.Application.Feature.Category.Queries.Validation
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
                .MustAsync(async (id, CancellationToken) => await _unitOfWork.Categories.IsAnyExistAsync(r => r.Id == id)).WithMessage("Category Is Not Exist");
        }
    }
}
