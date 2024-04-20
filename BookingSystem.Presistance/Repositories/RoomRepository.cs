using BookingSystem.Application.IRepositories;
using BookingSystem.Domain.Model;
using BookingSystem.Presistance.Data;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Presistance.Repositories
{
    public class RoomRepository : Repository<Room>, IRoomRepository
    {
        private readonly BookingDbContext _context;

        public RoomRepository(BookingDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> GetRoomIdByUserIdAsync(string userId) =>
            await _context.Users.Where(u => u.Id == userId).Include(u => u.Reservation).ThenInclude(r => r!.Rooms).Select(se => se.Reservation!.Rooms!.First().Id).FirstAsync();

        public async Task MakeReservedAsync(string roomId, int reserveId)
        {
            int id = int.Parse(roomId);
            await _context.Rooms
                    .Where(u => u.Id == id)
                    .ExecuteUpdateAsync(exc => exc.SetProperty(prop => prop.ReservationId, reserveId));
        }

        public async Task MakeRoomIsBookedById(int roomId) =>
            await _context.Rooms.Where(r => r.Id == roomId).ExecuteUpdateAsync(room => room.SetProperty(prop => prop.IsBooked, true));
    }
}
