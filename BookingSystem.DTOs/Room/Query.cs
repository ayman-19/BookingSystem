namespace BookingSystem.DTOs.Room
{
    public record Query
    {
        public string Code { get; set; }
        public bool IsBooked { get; set; }
        public int Floor { get; set; }
    }
}
