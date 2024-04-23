using BookingSystem.Application.Feature.Foods.Commands.Request;
using BookingSystem.Application.IRepositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BookingSystem.Application.Feature.Foods.Commands.Handler
{
    public class UpdateHandler : IRequestHandler<UpdateRequest, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContext;
        public UpdateHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContext)
        {
            _unitOfWork = unitOfWork;
            _httpContext = httpContext;
        }

        public async Task<string> Handle(UpdateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = _httpContext.HttpContext.User.Claims.First(t => t.Type == ClaimTypes.PrimarySid).Value;
                var roomId = await _unitOfWork.Rooms.GetRoomIdByUserIdAsync(userId);
                var food = await _unitOfWork.Foods.GetAsync(f => f.ProductId == request.Command.ProductId && f.RoomId == roomId, astracking: true);
                food.Amount = request.Command.Amount;
                await _unitOfWork.Products.UpdateQuantityAsync(request.Command.ProductId, (request.Command.Amount - food.Amount));
                await _unitOfWork.SaveChanges();
                await _unitOfWork.CommitAsync();
                return "Updated";
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw new Exception(ex.Message);
            }
        }
    }
}
