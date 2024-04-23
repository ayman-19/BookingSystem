using BookingSystem.DTOs.Foods;
using MediatR;

namespace BookingSystem.Application.Feature.Foods.Qeuries.Request
{
    public record CheckOutRequest(int roomId) : IRequest<FoodQuery>;
}
