using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Domain.Model
{
    [PrimaryKey(nameof(ProductId), nameof(RoomId))]
    public class Food
    {
        public int ProductId { get; set; }
        public int RoomId { get; set; }
        public int Amount { get; set; }
        public Product? Product { get; set; }
        public Room? Room { get; set; }
    }
}
