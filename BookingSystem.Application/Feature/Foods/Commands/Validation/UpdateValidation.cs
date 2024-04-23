using BookingSystem.Application.Feature.Foods.Commands.Request;
using BookingSystem.Application.IRepositories;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BookingSystem.Application.Feature.Foods.Commands.Validation
{
    public class UpdateValidation : AbstractValidator<UpdateRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContext;
        public UpdateValidation(IUnitOfWork unitOfWork, IHttpContextAccessor httpContext)
        {
            _unitOfWork = unitOfWork;
            _httpContext = httpContext;
            CustomValidation();
        }

        private async Task CustomValidation()
        {

            var userId = _httpContext.HttpContext.User.Claims.First(t => t.Type == ClaimTypes.PrimarySid).Value;
            var roomId = await _unitOfWork.Rooms.GetRoomIdByUserIdAsync(userId);
            RuleFor(req => req.Command.Amount)
                .NotEmpty().WithMessage("Amount Not Empty")
                .NotNull().WithMessage("Amount Not Null")
                .GreaterThan(0).WithMessage("Enter Postive Amount");

            RuleFor(req => req.Command.ProductId)
                .NotEmpty().WithMessage("Product Id Not Empty")
                .NotNull().WithMessage("Product Id Not Null")
                .MustAsync(async (id, CancellationToken) => await _unitOfWork.Products.IsAnyExistAsync(i => i.Id == id)).WithMessage("Product Is Not Exist!");

            RuleFor(req => req.Command)
                .MustAsync(async (req, CancellationToken) => await _unitOfWork.Products.IsAnyExistAsync(i => i.Id == req.ProductId && i.Quantity >= (req.Amount - _unitOfWork.Foods.GetAmountExist(req.ProductId, roomId)))).WithMessage("Enter Amount Is Valid!");
        }
    }
}
