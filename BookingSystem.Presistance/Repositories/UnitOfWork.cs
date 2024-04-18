using BookingSystem.Application.IRepositories;
using BookingSystem.Presistance.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace BookingSystem.Presistance.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookingDbContext _context;
        private readonly IDbContextTransaction _dbContextTransaction;

        public UnitOfWork(BookingDbContext context, IUserRepository users,
            IReservationRepository reservations, IRoomRepository rooms)
        {
            _context = context;
            _dbContextTransaction = _context.Database.BeginTransaction();
            Users = users;
            Reservations = reservations;
            Rooms = rooms;
        }
        public IUserRepository Users { get; set; }
        public IReservationRepository Reservations { get; set; }
        public IRoomRepository Rooms { get; set; }

        public Task CommitAsync() => _dbContextTransaction.CommitAsync();

        public void Dispose() => _dbContextTransaction.Dispose();

        public Task RollbackAsync() => _dbContextTransaction.RollbackAsync();

        public Task<int> SaveChanges() => _context.SaveChangesAsync();
    }
}
