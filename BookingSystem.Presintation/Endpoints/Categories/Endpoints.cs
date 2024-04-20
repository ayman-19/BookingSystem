using BookingSystem.Application.Feature.Category.Commands.Request;
using BookingSystem.Application.Feature.Category.Queries.Request;
using BookingSystem.DTOs.Category;
using MediatR;

namespace BookingSystem.Presintation.Endpoints.Categories
{
    public static class Endpoints
    {
        public static void MapCategoryEntpoints(this IEndpointRouteBuilder builder)
        {
            var map = builder.MapGroup("Categories");
            map.MapPost("CreateAsync", async (CategoryCommand command, ISender _sender) =>
            {
                return await _sender.Send(new CreateRequest(command));
            });
            map.MapPut("UpdateAsync/{id}", async (int id, CategoryCommand command, ISender _sender) =>
            {
                return await _sender.Send(new UpdateRequest(id, command));
            });
            map.MapDelete("DeleteAsync/{id}", async (int id, ISender _sender) =>
            {
                return await _sender.Send(new DeleteRequest(id));
            });
            map.MapGet("GatAllAsync", async (ISender _sender) =>
            {
                return await _sender.Send(new GetAllRequest());
            });
            map.MapGet("GatByIdAsync", async (int id, ISender _sender) =>
            {
                return await _sender.Send(new GetByIdRequest(id));
            });

        }
    }
}
