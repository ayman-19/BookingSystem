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

        public async Task MakeReservedAsync(string userId, int reserveId)
            => await _context.Users
                .Where(u => u.Id == userId)
                .ExecuteUpdateAsync(exc => exc.SetProperty(prop => prop.ReservationId, reserveId));
    }
}
