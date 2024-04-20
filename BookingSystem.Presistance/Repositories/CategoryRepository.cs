using BookingSystem.Application.IRepositories;
using BookingSystem.Domain.Model;
using BookingSystem.Presistance.Data;

namespace BookingSystem.Presistance.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly BookingDbContext context;

        public CategoryRepository(BookingDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<string> GetNameByIdAsync(int id)
            => (await context.Categories.FindAsync(id))!.Name;
    }
}
