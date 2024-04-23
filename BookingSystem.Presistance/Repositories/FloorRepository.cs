using BookingSystem.Application.IRepositories;
using BookingSystem.Domain.Model;
using BookingSystem.Presistance.Data;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Presistance.Repositories
{
    public class FloorRepository : Repository<Floor>, IFloorRepository
    {
        private readonly BookingDbContext context;

        public FloorRepository(BookingDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<int> GetNumberAsync(int id)
        => await context.Floors.Where(f => f.Id == id).Select(se => se.Number).FirstAsync();
    }
}
