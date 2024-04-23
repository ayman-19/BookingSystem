using BookingSystem.Application.Feature.Floors.Queries.Request;
using BookingSystem.Application.IRepositories;
using FluentValidation;

namespace BookingSystem.Application.Feature.Floors.Queries.Validation
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
                .MustAsync(async (id, CancellationToken) => await _unitOfWork.Floors.IsAnyExistAsync(r => r.Id == id)).WithMessage("Floor Is Not Exist");
        }
    }
}
