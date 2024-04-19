namespace BookingSystem.Application.IRepositories
{
    public interface ISharedBetweenUserAndRoomRepository
    {
        Task MakeReservedAsync(string Id, int reserveId);
    }
}
