using BookingSystem.Application.IRepositories;
using BookingSystem.Domain.Model;
using BookingSystem.Presistance.Data;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Presistance.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly BookingDbContext _context;

        public UserRepository(BookingDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<string> GetNameByIdAsync(string id)
            => await _context.Users.Where(user => user.Id == id).Select(name => name.Name).FirstAsync();

        public async Task MakeReservedAsync(string userId, int reserveId)
            => await _context.Users
                .Where(u => u.Id == userId)
                .ExecuteUpdateAsync(exc => exc.SetProperty(prop => prop.ReservationId, reserveId));

        public void RemoveCallback(object state)
        {
            lock (_context)
            {
                if (_context.Users.Any(u => u.ConfirmCreation == null && u.Creation.AddMinutes(2) <= DateTime.Now))
                    _context.Users.Where(u => u.ConfirmCreation == null && u.Creation.AddMinutes(2) <= DateTime.Now)
                        .ExecuteDelete();
            }
        }

        public async Task SetRoomIdAsync(string id, int roomId)
            => await _context.Users.Where(u => u.Id == id).ExecuteUpdateAsync(prop => prop.SetProperty(up => up.RoomId, roomId));
    }
}
