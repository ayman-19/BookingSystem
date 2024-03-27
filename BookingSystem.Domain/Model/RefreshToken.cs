using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Domain.Model
{
	[Owned]
	public class RefreshToken
	{
		public string AccessToken { get; set; }
		public DateTime AccessTokenExpiration { get; set; }
		public string RefreshJwtToken { get; set; }
		public bool IsExpire => ExpireOn <= DateTime.Now;
		public bool IsValid => !IsExpire && RevokeOn == null;
		public DateTime CreateOn { get; set; }
		public DateTime ExpireOn { get; set; }
		public DateTime? RevokeOn { get; set; }
	}
}
