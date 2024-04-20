namespace BookingSystem.Application.IRepositories
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public IUserRepository Users { get; set; }
        public IReservationRepository Reservations { get; set; }
        public IRoomRepository Rooms { get; set; }
        public IFloorRepository Floors { get; set; }
        public ICategoryRepository Categories { get; set; }
        public IProductRepository Products { get; set; }
        Task<int> SaveChanges();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
