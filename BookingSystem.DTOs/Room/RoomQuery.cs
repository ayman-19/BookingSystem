namespace BookingSystem.DTOs.Room
{
    public record RoomQuery
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public bool IsBooked { get; set; }
        public int Floor { get; set; }
    }
}
