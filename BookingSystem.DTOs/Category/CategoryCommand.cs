namespace BookingSystem.DTOs.Category
{
    public record CategoryCommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
