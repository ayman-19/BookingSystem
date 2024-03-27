namespace BookingSystem.Presistance.Helper
{
	public class jWTSettings
	{
		public string Key { get; set; }
		public string Issuer { get; set; }
		public string Audience { get; set; }
		public double AccessTokenExiretionDate { get; set; }
		public double RefreshTokenExiretionDate { get; set; }
	}
}
