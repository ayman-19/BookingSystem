using BookingSystem.DTOs.Product;
using MediatR;

namespace BookingSystem.Application.Feature.Product.Queries.Request
{
    public record GetByIdRequest(int id) : IRequest<ProductQuery>;
}
