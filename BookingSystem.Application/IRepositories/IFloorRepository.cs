﻿using BookingSystem.Domain.Model;

namespace BookingSystem.Application.IRepositories
{
    public interface IFloorRepository : IRepository<Floor>
    {
        Task<int> GetNumberAsync(int id);
    }
}
