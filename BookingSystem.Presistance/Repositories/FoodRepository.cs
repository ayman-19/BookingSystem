using BookingSystem.Application.IRepositories;
using BookingSystem.Domain.Model;
using BookingSystem.Presistance.Data;

namespace BookingSystem.Presistance.Repositories
{
    public class FoodRepository : Repository<Food>, IFoodRepository
    {
        public FoodRepository(BookingDbContext context) : base(context)
        {
        }
    }
}
