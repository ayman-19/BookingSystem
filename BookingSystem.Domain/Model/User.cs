using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSystem.Domain.Model
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public int? ReservationId { get; set; }
        public int? RoomId { get; set; }
        [ForeignKey(nameof(RoomId))]
        public Room? Room { get; set; }
        public string Code { get; set; } = string.Empty;
        public DateTime? ConfirmCreation { get; set; }
        public DateTime Creation { get; set; } = DateTime.Now;
        [ForeignKey(nameof(ReservationId))]
        public Reservation? Reservation { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
