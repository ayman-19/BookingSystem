namespace BookingSystem.Application.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        public IUserRepository Users { get; set; }
        public IReservationRepository Reservations { get; set; }
        public IRoomRepository Rooms { get; set; }
        Task<int> SaveChanges();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
