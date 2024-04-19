using BookingSystem.DTOs.Room;
using MediatR;

namespace BookingSystem.Application.Feature.Rooms.Queries.Request
{
    public record GetByIdRequest(int id) : IRequest<RoomQuery>;
}
