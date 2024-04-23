using BookingSystem.DTOs.Floors;
using MediatR;

namespace BookingSystem.Application.Feature.Floors.Commnds.Request
{
    public record DeleteRequest(int id) : IRequest<FloorQuery>;
}
