using BookingSystem.Application.Feature.Reservation.Commands.Request;
using BookingSystem.DTOs.Reservation;
using MediatR;

namespace BookingSystem.Presintation.Endpoints.Reservations
{
    public static class Endpoints
    {
        public static void MapReservationEntpoints(this IEndpointRouteBuilder builder)
        {
            var map = builder.MapGroup("Reservations");
            map.MapPost("CreateAsync", async (ReservationCommand command, ISender _sender) =>
            {
                return await _sender.Send(new CreateRequest(command));
            });
            map.MapPut("UpdateAsync/{id}", async (int id, ReservationCommand command, ISender _sender) =>
            {
                return await _sender.Send(new UpdateRequest(id, command));
            });
            map.MapDelete("DeleteAsync/{id}", async (int id, ISender _sender) =>
            {
                return await _sender.Send(new DeleteRequest(id));
            });
        }
    }
}
