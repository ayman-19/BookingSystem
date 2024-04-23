using BookingSystem.Application.Feature.Foods.Commands.Request;
using BookingSystem.Application.IRepositories;
using BookingSystem.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BookingSystem.Application.Feature.Foods.Commands.Handler
{
    public class CreateHandler : IRequestHandler<CreateRequest, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContext;
        public CreateHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContext)
        {
            _unitOfWork = unitOfWork;
            _httpContext = httpContext;
        }
        public async Task<string> Handle(CreateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = _httpContext.HttpContext.User.Claims.First(t => t.Type == ClaimTypes.PrimarySid).Value;
                var roomId = await _unitOfWork.Rooms.GetRoomIdByUserIdAsync(userId);
                await _unitOfWork.Foods.AddAsync(new Food { ProductId = request.Command.ProductId, Amount = request.Command.Amount, RoomId = roomId });
                await _unitOfWork.Products.UpdateQuantityAsync(request.Command.ProductId, request.Command.Amount);
                await _unitOfWork.SaveChanges();
                await _unitOfWork.CommitAsync();
                return "Added";
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw new Exception(ex.Message);
            }

        }
    }
}
