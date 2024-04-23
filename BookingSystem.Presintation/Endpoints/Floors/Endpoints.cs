using BookingSystem.Application.Feature.Floors.Commnds.Request;
using BookingSystem.Application.Feature.Floors.Queries.Request;
using BookingSystem.DTOs.Floors;
using MediatR;

namespace BookingSystem.Presintation.Endpoints.Floors
{
    public static class Endpoints
    {
        public static void MapFloorEntpoints(this IEndpointRouteBuilder builder)
        {
            var map = builder.MapGroup("Floors");
            map.MapPost("CreateAsync", async (FloorCommand command, ISender _sender) =>
            {
                return await _sender.Send(new CreateRequest(command));
            });
            map.MapPut("UpdateAsync/{id}", async (int id, FloorCommand command, ISender _sender) =>
            {
                return await _sender.Send(new UpdateRequest(id, command));
            });
            map.MapDelete("DeleteAsync/{id}", async (int id, ISender _sender) =>
            {
                return await _sender.Send(new DeleteRequest(id));
            });
            map.MapGet("GetAllAsync", async (ISender _sender) =>
            {
                return await _sender.Send(new GetAllRequest());
            });
            map.MapGet("GetByIdAsync/{id}", async (int id, ISender _sender) =>
            {
                return await _sender.Send(new GetByIdRequest(id));
            });
        }
    }
}
