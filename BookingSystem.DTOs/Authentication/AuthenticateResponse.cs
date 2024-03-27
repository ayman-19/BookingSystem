namespace BookingSystem.DTOs.Authentication
{
	public record AuthenticateResponse
	{
		public string UserId { get; set; } = string.Empty;
		public string Massage { get; set; } = string.Empty;
		public bool IsAuthanticated { get; set; } = false;
		public string UserName { get; set; } = string.Empty;
		public string AccessToken { get; set; } = string.Empty;
		public string RefreshToken { get; set; } = string.Empty;
		public DateTime AccessTokenExpireOn { get; set; }
		public DateTime RefreshTokenExpireOn { get; set; }
	}
}
