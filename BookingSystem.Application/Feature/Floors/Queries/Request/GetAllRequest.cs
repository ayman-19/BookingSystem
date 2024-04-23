using BookingSystem.DTOs.Floors;
using MediatR;

namespace BookingSystem.Application.Feature.Floors.Queries.Request
{
    public record GetAllRequest() : IRequest<List<FloorQuery>>;
}
