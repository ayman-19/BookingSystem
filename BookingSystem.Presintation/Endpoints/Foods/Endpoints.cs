using BookingSystem.Application.Feature.Foods.Commands.Request;
using BookingSystem.Application.Feature.Foods.Qeuries.Request;
using BookingSystem.Application.IRepositories;
using BookingSystem.DTOs.Foods;
using MediatR;
using System.Security.Claims;

namespace BookingSystem.Presintation.Endpoints.Foods
{
    public static class Endpoints
    {
        public static void MapFoodEntpoints(this IEndpointRouteBuilder builder)
        {
            var map = builder.MapGroup("Foods");
            map.MapGet("CheckOutAsync", async (IUnitOfWork _unitOfWork, IHttpContextAccessor _httpContext, ISender _sender) =>
            {
                var userId = _httpContext.HttpContext!.User.Claims.First(t => t.Type == ClaimTypes.PrimarySid).Value;
                var roomId = await _unitOfWork.Rooms.GetRoomIdByUserIdAsync(userId);
                return await _sender.Send(new CheckOutRequest(roomId));
            });
            map.MapPost("RequestAsync", async (FoodCommand Command, ISender _sender) =>
            {
                return await _sender.Send(new CreateRequest(Command));
            });
            map.MapPost("UpdateRequestAsync", async (FoodCommand Command, ISender _sender) =>
            {
                return await _sender.Send(new UpdateRequest(Command));
            });
        }
    }
}
