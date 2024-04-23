using BookingSystem.Domain.Model;

namespace BookingSystem.Application.IRepositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task UpdateQuantityAsync(int id, int amount);
        double GetPrice(int id);
    }
}
