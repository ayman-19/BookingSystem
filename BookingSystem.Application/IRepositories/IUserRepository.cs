using BookingSystem.Domain.Model;

namespace BookingSystem.Application.IRepositories
{
    public interface IUserRepository : IRepository<User>, ISharedBetweenUserAndRoomRepository
    {
        Task<string> GetNameByIdAsync(string id);
        Task SetRoomIdAsync(string id, int roomId);
        void RemoveCallback(object state);
    }
}
