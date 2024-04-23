namespace BookingSystem.DTOs.Foods
{
    public record FoodQuery
    {
        public string Name { get; set; }
        public string Room { get; set; }
        public double Total { get; set; }
    }
}
