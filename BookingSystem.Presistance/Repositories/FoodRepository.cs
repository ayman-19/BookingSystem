using BookingSystem.Application.IRepositories;
using BookingSystem.Domain.Model;
using BookingSystem.Presistance.Data;

namespace BookingSystem.Presistance.Repositories
{
    public class FoodRepository : Repository<Food>, IFoodRepository
    {
        private readonly BookingDbContext context;

        public FoodRepository(BookingDbContext context) : base(context)
        {
            this.context = context;
        }

        public int GetAmountExist(int productId, int roomId)
            => context.Foods.Where(f => f.ProductId == productId && f.RoomId == roomId).Select(f => f.Amount).First();
    }
}
