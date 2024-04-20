using BookingSystem.DTOs.Product;
using MediatR;

namespace BookingSystem.Application.Feature.Product.Commands.Request
{
    public record DeleteRequest(int id) : IRequest<ProductQuery>;
}
