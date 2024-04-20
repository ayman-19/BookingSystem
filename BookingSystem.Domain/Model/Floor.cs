﻿namespace BookingSystem.Domain.Model
{
    public class Floor
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public List<Room>? Rooms { get; set; }
    }
}
