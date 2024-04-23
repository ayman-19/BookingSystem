using BookingSystem.DTOs.Foods;
using MediatR;

namespace BookingSystem.Application.Feature.Foods.Commands.Request
{
    public record UpdateRequest(FoodCommand Command) : IRequest<string>;
}
