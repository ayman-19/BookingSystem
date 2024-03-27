namespace BookingSystem.DTOs.Authentication
{
	public record RegisterRequest
	{
		public string Name { get; set; }
		public string Address { get; set; }
		public DateTime BirthDate { get; set; }
		public string Phone { get; set; }
		public string UsarName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }
	}
}
