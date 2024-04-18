using BookingSystem.DTOs.Room;
using MediatR;

namespace BookingSystem.Application.Feature.Rooms.Commands.Request
{
    public record UpdateRequest(int id, Command Command) : IRequest<Query>;
}
