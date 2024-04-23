using BookingSystem.DTOs.Authentication;

namespace BookingSystem.DTOs.Reservation
{
    public record ReservationQuery
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreateOn { get; set; }
        public string Description { get; set; }
        public List<UserReservedRespose> Users { get; set; }
    }
}
