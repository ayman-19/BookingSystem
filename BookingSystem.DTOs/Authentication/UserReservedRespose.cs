namespace BookingSystem.DTOs.Authentication
{
    public record UserReservedRespose
    {
        public string Name { get; set; }
        public int Floor { get; set; }
        public string Room { get; set; }
        public DateTime Date { get; set; }
    }
}
