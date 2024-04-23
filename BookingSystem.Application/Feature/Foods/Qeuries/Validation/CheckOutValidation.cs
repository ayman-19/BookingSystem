using BookingSystem.Application.Feature.Foods.Qeuries.Request;
using BookingSystem.Application.IRepositories;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace BookingSystem.Application.Feature.Foods.Qeuries.Validation
{
    public class CheckOutValidation : AbstractValidator<CheckOutRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContext;
        public CheckOutValidation(IUnitOfWork unitOfWork, IHttpContextAccessor httpContext)
        {
            _unitOfWork = unitOfWork;
            _httpContext = httpContext;
            CustomValidation();
        }

        private void CustomValidation()
        {

            RuleFor(req => req.roomId)
                 .NotEmpty().WithMessage("Room Id Not Empty")
                .NotNull().WithMessage("Room Id Not Null")
                .MustAsync(async (id, CancellationToken) => await _unitOfWork.Foods.IsAnyExistAsync(f => f.RoomId == id))
                .WithMessage("Room Not Request Food");
        }
    }
}
