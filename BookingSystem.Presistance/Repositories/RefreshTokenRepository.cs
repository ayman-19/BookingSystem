using BookingSystem.Application.IRepositories;
using BookingSystem.Domain.Model;
using BookingSystem.Presistance.Data;

namespace BookingSystem.Presistance.Repositories
{
	public class RefreshTokenRepository : Repository<RefreshToken>, IRefreshTokenRepository
	{
		public RefreshTokenRepository(BookingDbContext context) : base(context)
		{
		}
	}
}
