using BookingSystem.Application.Feature.User.Commands.Request;
using BookingSystem.Application.IAuthentication;
using BookingSystem.Application.IRepositories;
using MediatR;

namespace BookingSystem.Application.Feature.User.Commands.Handler
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserRequest>
    {
        private readonly IAccount _account;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserHandler(IAccount account, IUnitOfWork unitOfWork)
        {
            _account = account;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            var roomId = await _account.DeleteUserAsync(request.userId);
            await _unitOfWork.Rooms.MakeRoomIsNotBookedByUserAsync(roomId);
        }
    }
}
