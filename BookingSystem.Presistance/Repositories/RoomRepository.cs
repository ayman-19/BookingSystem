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

        public async Task<int> GetRoomIdByCode(string code) => await _context.Rooms.Where(r => r.Code == code).Select(r => r.Id).FirstAsync();
        public async Task MakeRoomIsBookedById(int roomId) =>
            await _context.Rooms.Where(r => r.Id == roomId).ExecuteUpdateAsync(room => room.SetProperty(prop => prop.IsBooked, true));
    }
}
