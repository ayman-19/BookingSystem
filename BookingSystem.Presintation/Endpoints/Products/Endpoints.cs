using BookingSystem.Application.Feature.Product.Commands.Request;
using BookingSystem.Application.Feature.Product.Queries.Request;
using BookingSystem.DTOs.Product;
using MediatR;

namespace BookingSystem.Presintation.Endpoints.Products
{
    public static class Endpoints
    {
        public static void MapProductEntpoints(this IEndpointRouteBuilder builder)
        {
            var map = builder.MapGroup("Products");
            map.MapPost("CreateAsync", async (ProductCommand command, ISender _sender) =>
            {
                return await _sender.Send(new CreateRequest(command));
            });
            map.MapPut("UpdateAsync/{id}", async (int id, ProductCommand command, ISender _sender) =>
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
            }).RequireRateLimiting("fixed-by-user");
            map.MapGet("GetByIdAsync/{id}", async (int id, ISender _sender) =>
            {
                return await _sender.Send(new GetByIdRequest(id));
            });
        }
    }
}
