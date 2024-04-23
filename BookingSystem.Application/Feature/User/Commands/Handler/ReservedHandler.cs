using BookingSystem.Application.Feature.User.Commands.Request;
using BookingSystem.Application.IRepositories;
using BookingSystem.DTOs.Authentication;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BookingSystem.Application.Feature.User.Commands.Handler
{
    public class ReservedHandler : IRequestHandler<ReservedRequest, UserReservedRespose>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContext;
        public ReservedHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContext)
        {
            _unitOfWork = unitOfWork;
            _httpContext = httpContext;
        }
        public async Task<UserReservedRespose> Handle(ReservedRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var claim = _httpContext.HttpContext.User.Claims;
                var userId = claim.First(t => t.Type == ClaimTypes.PrimarySid).Value;
                var room = await _unitOfWork.Rooms.GetNameAndFloorValidAsync();
                if (room.Item1 == null)
                    throw new Exception("Not Found Valid Now!");
                await _unitOfWork.Users.MakeReservedAsync(userId, request.reservationId);
                await _unitOfWork.Rooms.MakeReservedAsync(room.Item1, request.reservationId);
                await _unitOfWork.Rooms.MakeRoomIsBookedByCodeAsync(room.Item1);
                await _unitOfWork.Users.SetRoomIdAsync(userId, room.Item3);
                var reservation = await _unitOfWork.Reservations.GetAsync(r => r.Id == request.reservationId);
                return new UserReservedRespose
                {
                    Date = reservation.Date,
                    Floor = room.Item2,
                    Room = room.Item1,
                    Name = claim.First(t => t.Type == ClaimTypes.Name).Value
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
