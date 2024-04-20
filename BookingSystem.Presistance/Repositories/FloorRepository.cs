using BookingSystem.Application.IRepositories;
using BookingSystem.Domain.Model;
using BookingSystem.Presistance.Data;

namespace BookingSystem.Presistance.Repositories
{
    public class FloorRepository : Repository<Floor>, IFloorRepository
    {
        public FloorRepository(BookingDbContext context) : base(context)
        {
        }
    }
}
