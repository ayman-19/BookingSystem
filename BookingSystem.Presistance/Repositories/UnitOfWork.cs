using BookingSystem.Application.IRepositories;
using BookingSystem.Presistance.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace BookingSystem.Presistance.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly BookingDbContext _context;
		private readonly IDbContextTransaction _dbContextTransaction;

		public UnitOfWork(BookingDbContext context, IUserRepository users)
		{
			_context = context;
			_dbContextTransaction = _context.Database.BeginTransaction();
			Users = users;
		}
		public IUserRepository Users { get; set; }

		public Task CommitAsync() => _dbContextTransaction.CommitAsync();

		public void Dispose() => _dbContextTransaction.Dispose();

		public Task RollbackAsync() => _dbContextTransaction.RollbackAsync();

		public Task<int> SaveChanges() => _context.SaveChangesAsync();
	}
}
