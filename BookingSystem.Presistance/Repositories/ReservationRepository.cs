using BookingSystem.Application.IRepositories;
using BookingSystem.Domain.Model;
using BookingSystem.Presistance.Data;

namespace BookingSystem.Presistance.Repositories
{
    public class ReservationRepository : Repository<Reservation>, IReservationRepository
    {
        private readonly BookingDbContext _context;

        public ReservationRepository(BookingDbContext context) : base(context)
        {
            _context = context;
        }

    }
}
