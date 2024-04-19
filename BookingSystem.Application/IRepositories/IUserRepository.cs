using BookingSystem.Domain.Model;

namespace BookingSystem.Application.IRepositories
{
    public interface IUserRepository : IRepository<User>, ISharedBetweenUserAndRoomRepository
    {
    }
}
