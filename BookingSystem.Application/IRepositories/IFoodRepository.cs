using BookingSystem.Domain.Model;

namespace BookingSystem.Application.IRepositories
{
    public interface IFoodRepository : IRepository<Food>
    {
        int GetAmountExist(int productId, int roomId);
    }
}
