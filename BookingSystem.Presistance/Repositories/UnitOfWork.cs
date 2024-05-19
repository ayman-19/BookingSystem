using BookingSystem.Application.IRepositories;
using BookingSystem.Presistance.Data;

namespace BookingSystem.Presistance.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookingDbContext _context;
        //private readonly IDbContextTransaction _dbContextTransaction;

        public UnitOfWork(BookingDbContext context, IUserRepository users,
            IReservationRepository reservations, IRoomRepository rooms, IFloorRepository floors, ICategoryRepository categories, IProductRepository products, IFoodRepository foods)
        {
            _context = context;
            //_dbContextTransaction = _context.Database.BeginTransaction();
            Users = users;
            Reservations = reservations;
            Rooms = rooms;
            Floors = floors;
            Categories = categories;
            Products = products;
            Foods = foods;
        }
        public IUserRepository Users { get; set; }
        public IReservationRepository Reservations { get; set; }
        public IRoomRepository Rooms { get; set; }
        public IFloorRepository Floors { get; set; }
        public ICategoryRepository Categories { get; set; }
        public IProductRepository Products { get; set; }
        public IFoodRepository Foods { get; set; }

        public Task CommitAsync() { return Task.CompletedTask; /*_dbContextTransaction.CommitAsync()*/}

        public async ValueTask DisposeAsync() =>
            await _context.DisposeAsync();
        public Task RollbackAsync()
        {
            return Task.CompletedTask; /*_dbContextTransaction.RollbackAsync();*/
        }

        public Task<int> SaveChanges() => _context.SaveChangesAsync();
    }
}
