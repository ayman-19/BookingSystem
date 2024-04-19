using BookingSystem.Domain.SoftDeletable;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSystem.Domain.Model
{
    public class Reservation : ISoftDelete
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreateOn { get; set; } = DateTime.UtcNow;
        public string Description { get; set; }
        public List<User> Users { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedOn { get; set; }
        public List<Room>? Rooms { get; set; }
    }
}
