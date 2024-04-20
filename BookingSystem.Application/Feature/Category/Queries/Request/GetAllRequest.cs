using BookingSystem.DTOs.Category;
using MediatR;

namespace BookingSystem.Application.Feature.Category.Queries.Request
{
    public record GetAllRequest : IRequest<List<CategoryQuery>>;
}
