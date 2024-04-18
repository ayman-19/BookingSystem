using BookingSystem.DTOs.Room;
using MediatR;

namespace BookingSystem.Application.Feature.Rooms.Commands.Request
{
    public record DeleteRequest(int id) : IRequest<Query>;
}
