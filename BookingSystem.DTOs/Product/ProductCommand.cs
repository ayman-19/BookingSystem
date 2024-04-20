namespace BookingSystem.DTOs.Product
{
    public record ProductCommand
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
    }
}
