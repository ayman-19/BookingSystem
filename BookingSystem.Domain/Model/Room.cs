namespace BookingSystem.Domain.Model
{
    public class Room
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public bool IsBooked { get; set; }
        public int Floor { get; set; }
        public Reservation? Reservation { get; set; }
    }
}
