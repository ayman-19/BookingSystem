using BookingSystem.Domain.SoftDeletable;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSystem.Domain.Model
{
    public class Reservation : ISoftDelete
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PersonCount { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreateOn { get; set; } = DateTime.UtcNow;
        public string Description { get; set; }
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedOn { get; set; }
        public int RomeId { get; set; }
        [ForeignKey(nameof(RomeId))]
        public Room? Room { get; set; }
    }
}
