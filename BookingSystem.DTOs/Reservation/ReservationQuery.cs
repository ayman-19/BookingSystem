namespace BookingSystem.DTOs.Reservation
{
    public record ReservationQuery
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreateOn { get; set; } = DateTime.UtcNow;
        public string Description { get; set; }
    }
}
