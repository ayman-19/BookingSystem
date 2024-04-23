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

        public async Task<(string, int, int)> GetNameAndFloorValidAsync()
        {
            var result = await _context.Rooms.Where(r => !r.IsBooked).Include(c => c.Floor).Select(se => new { Name = se.Code, Floor = se.Floor.Number, Id = se.Id }).FirstAsync();
            return (result.Name, result.Floor, result.Id);
        }

        public string GetNameById(int id)
            => _context.Rooms.Where(r => r.Id == id).Select(r => r.Code).First();

        public async Task<int> GetRoomIdByUserIdAsync(string userId)
        {
            var result = _context.Users.Where(u => u.Id == userId).Include(u => u.Room);
            if (result.Any(r => r.Room == null))
                return 0;
            return await result.Select(se => se.Room!.Id).FirstAsync();
        }


        public async Task MakeReservedAsync(string code, int reserveId) =>
            await _context.Rooms
                    .Where(u => u.Code == code)
                    .ExecuteUpdateAsync(exc => exc.SetProperty(prop => prop.ReservationId, reserveId));

        public async Task MakeRoomIsBookedByCodeAsync(string code) =>
            await _context.Rooms.Where(r => r.Code == code).ExecuteUpdateAsync(room => room.SetProperty(prop => prop.IsBooked, true));

        public async Task MakeRoomIsNotBookedByReservationAsync(int reservationId)
        => await _context.Rooms.Where(room => room.ReservationId == reservationId)
            .ExecuteUpdateAsync(se => se.SetProperty(prop => prop.IsBooked, false));

        public async Task MakeRoomIsNotBookedByUserAsync(int roomId)
           => await _context.Rooms.Where(room => room.Id == roomId)
            .ExecuteUpdateAsync(se => se.SetProperty(prop => prop.IsBooked, false));
    }
}
