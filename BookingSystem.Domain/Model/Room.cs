using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSystem.Domain.Model
{
    public class Room
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public bool IsBooked { get; set; }
        public int Floor { get; set; }
        public int? ReservationId { get; set; }
        [ForeignKey(nameof(ReservationId))]
        public Reservation? Reservation { get; set; }
    }
}
