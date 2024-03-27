namespace BookingSystem.Application.IRepositories
{
	public interface IUnitOfWork : IDisposable
	{
		public IUserRepository Users { get; set; }
		Task<int> SaveChanges();
		Task CommitAsync();
		Task RollbackAsync();
	}
}
