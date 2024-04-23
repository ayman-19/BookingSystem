using BookingSystem.DTOs.Room;

namespace BookingSystem.DTOs.Floors
{
    public record FloorDetails
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public List<RoomQuery>? Rooms { get; set; }
    }
}
