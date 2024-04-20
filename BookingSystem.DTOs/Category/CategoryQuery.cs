namespace BookingSystem.DTOs.Category
{
    public record CategoryQuery
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
