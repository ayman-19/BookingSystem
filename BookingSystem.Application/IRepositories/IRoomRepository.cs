using BookingSystem.Domain.Model;

namespace BookingSystem.Application.IRepositories
{
    public interface IRoomRepository : IRepository<Room>, ISharedBetweenUserAndRoomRepository
    {
        Task MakeRoomIsBookedById(int roomId);
        Task<int> GetRoomIdByUserIdAsync(string userId);
    }
}
