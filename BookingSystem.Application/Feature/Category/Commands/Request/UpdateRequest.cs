﻿using BookingSystem.DTOs.Category;
using MediatR;

namespace BookingSystem.Application.Feature.Category.Commands.Request
{
    public record UpdateRequest(int id, CategoryCommand Command) : IRequest<CategoryQuery>;
}
