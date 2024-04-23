namespace BookingSystem.DTOs.Foods
{
    public record FoodCommand
    {
        public int ProductId { get; set; }
        public int Amount { get; set; }
    }
}
