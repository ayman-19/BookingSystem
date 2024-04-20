using BookingSystem.Domain.Model;

namespace BookingSystem.Application.IRepositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<string> GetNameByIdAsync(int id);
    }
}
