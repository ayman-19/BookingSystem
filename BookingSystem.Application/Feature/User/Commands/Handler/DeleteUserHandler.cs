using BookingSystem.Application.Feature.User.Commands.Request;
using BookingSystem.Application.IAuthentication;
using MediatR;

namespace BookingSystem.Application.Feature.User.Commands.Handler
{
	public class DeleteUserHandler : IRequestHandler<DeleteUserRequest>
	{
		private readonly IAccount _account;

		public DeleteUserHandler(IAccount account)
		{
			_account = account;
		}
		public async Task Handle(DeleteUserRequest request, CancellationToken cancellationToken)
		=> await _account.DeleteUserAsync(request.userId);
	}
}
