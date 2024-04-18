using BookingSystem.Domain.Model;

namespace BookingSystem.Application.IRepositories
{
    public interface IRoomRepository : IRepository<Room>
    {
        Task<int> GetRoomIdByCode(string code);
        Task MakeRoomIsBookedById(int roomId);
    }
}
