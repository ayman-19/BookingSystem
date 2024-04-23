using BookingSystem.Domain.Model;

namespace BookingSystem.Application.IRepositories
{
    public interface IRoomRepository : IRepository<Room>, ISharedBetweenUserAndRoomRepository
    {
        Task MakeRoomIsBookedByCodeAsync(string code);
        Task<int> GetRoomIdByUserIdAsync(string userId);
        Task<(string, int, int)> GetNameAndFloorValidAsync();
        Task MakeRoomIsNotBookedByReservationAsync(int reservationId);
        Task MakeRoomIsNotBookedByUserAsync(int roomId);
        string GetNameById(int id);
    }
}
