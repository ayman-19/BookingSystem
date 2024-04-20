using BookingSystem.DTOs.Product;
using MediatR;

namespace BookingSystem.Application.Feature.Product.Queries.Request
{
    public record GetAllRequest : IRequest<List<ProductQuery>>;
}
