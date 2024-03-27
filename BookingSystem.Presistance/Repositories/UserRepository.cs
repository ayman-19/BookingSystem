using BookingSystem.Application.IRepositories;
using BookingSystem.Domain.Model;
using BookingSystem.Presistance.Data;

namespace BookingSystem.Presistance.Repositories
{
	public class UserRepository : Repository<User>, IUserRepository
	{
		public UserRepository(BookingDbContext context) : base(context)
		{
		}
	}
}
