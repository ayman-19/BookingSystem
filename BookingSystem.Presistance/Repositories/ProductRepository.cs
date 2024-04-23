using BookingSystem.Application.IRepositories;
using BookingSystem.Domain.Model;
using BookingSystem.Presistance.Data;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Presistance.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly BookingDbContext context;

        public ProductRepository(BookingDbContext context) : base(context)
        {
            this.context = context;
        }

        public double GetPrice(int id)
            => context.Products.Where(p => p.Id == id).Select(p => p.Price).First();

        public async Task UpdateQuantityAsync(int id, int amount)
        => await context.Products.Where(p => p.Id == id).ExecuteUpdateAsync(exc => exc.SetProperty(prop => prop.Quantity, p => (p.Quantity - amount)));
    }
}
