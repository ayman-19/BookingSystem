using BookingSystem.DTOs.Floors;
using MediatR;

namespace BookingSystem.Application.Feature.Floors.Queries.Request
{
    public record GetByIdRequest(int id) : IRequest<FloorDetails>;
}
