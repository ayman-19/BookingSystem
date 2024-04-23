using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSystem.Domain.Model
{
    public class Room
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public bool IsBooked { get; set; }
        public int FloorId { get; set; }
        [ForeignKey(nameof(FloorId))]
        public Floor Floor { get; set; }
        public int? ReservationId { get; set; }
        [ForeignKey(nameof(ReservationId))]
        public Reservation? Reservation { get; set; }
        public List<Food>? Foods { get; set; }
        public User? User { get; set; }
    }
}
