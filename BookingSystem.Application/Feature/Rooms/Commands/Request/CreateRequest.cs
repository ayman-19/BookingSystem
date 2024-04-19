using BookingSystem.DTOs.Room;
using MediatR;

namespace BookingSystem.Application.Feature.Rooms.Commands.Request
{
    public record CreateRequest(RoomCommand Command) : IRequest<RoomQuery>;
}
