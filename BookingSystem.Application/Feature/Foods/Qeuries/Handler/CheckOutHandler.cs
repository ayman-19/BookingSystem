using BookingSystem.Application.Feature.Foods.Qeuries.Request;
using BookingSystem.Application.IRepositories;
using BookingSystem.DTOs.Foods;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BookingSystem.Application.Feature.Foods.Qeuries.Handler
{
    public class CheckOutHandler : IRequestHandler<CheckOutRequest, FoodQuery>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContext;

        public CheckOutHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContext)
        {
            _unitOfWork = unitOfWork;
            _httpContext = httpContext;
        }

        public async Task<FoodQuery> Handle(CheckOutRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var order = await _unitOfWork.Foods.GetAllAsync(f => f.RoomId == request.roomId);
                var result = order.Select(p => (_unitOfWork.Products.GetPrice(p.ProductId) * p.Amount));
                return new FoodQuery
                {
                    Room = _unitOfWork.Rooms.GetNameById(request.roomId),
                    Total = result.Sum(),
                    Name = _httpContext.HttpContext.User.Claims.First(t => t.Type == ClaimTypes.Name).Value
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
