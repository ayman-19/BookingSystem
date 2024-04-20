using BookingSystem.DTOs.Category;
using MediatR;

namespace BookingSystem.Application.Feature.Category.Commands.Request
{
    public record CreateRequest(CategoryCommand Command) : IRequest<CategoryQuery>;
}
