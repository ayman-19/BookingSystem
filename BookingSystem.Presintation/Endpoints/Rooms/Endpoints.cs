using BookingSystem.Application.Feature.Rooms.Commands.Request;
using BookingSystem.Application.Feature.Rooms.Queries.Request;
using BookingSystem.DTOs.Room;
using MediatR;

namespace BookingSystem.Presintation.Endpoints.Rooms
{
    public static class Endpoints
    {
        public static void MapRoomEntpoints(this IEndpointRouteBuilder builder)
        {
            var map = builder.MapGroup("Rooms");
            map.MapGet("GatAllAsync", async (ISender sender) => await sender.Send(new GetAllRequest()));
            map.MapPost("CreateAsync", async (RoomCommand command, ISender sender) => await sender.Send(new CreateRequest(command)));
            map.MapDelete("DeleteAsync/{id}", async (int id, ISender sender) => await sender.Send(new DeleteRequest(id)));
            map.MapPut("UpdateAsync/{id}", async (int id, RoomCommand command, ISender sender) => await sender.Send(new UpdateRequest(id, command)));
            map.MapGet("GatByIdAsync/{id}", async (int id, ISender sender) => await sender.Send(new GetByIdRequest(id)));

        }
    }
}
