using BookingSystem.DTOs.Foods;
using MediatR;

namespace BookingSystem.Application.Feature.Foods.Commands.Request
{
    public record CreateRequest(FoodCommand Command) : IRequest<string>;
}
