namespace BookingSystem.DTOs.Reservation
{
    public record ReservationCommand
    {
        public DateTime Date { get; set; }
        public DateTime CreateOn { get; set; } = DateTime.UtcNow;
        public string Description { get; set; }
    }
}
