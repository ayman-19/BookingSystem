using BookingSystem.Domain.SoftDeletable;
using Microsoft.AspNetCore.Identity;

namespace BookingSystem.Domain.Model
{
	public class User : IdentityUser, ISoftDelete
	{
		public string Name { get; set; }
		public string Address { get; set; }
		public DateTime BirthDate { get; set; }
		public bool IsDeleted { get; set; }
		public string Code { get; set; } = string.Empty;
		public DateTime DeletedOn { get; set; }
		public List<Reservation> Reservations { get; set; } = new List<Reservation>();
		public List<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
	}
}
