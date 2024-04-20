using BookingSystem.DTOs.Product;
using MediatR;

namespace BookingSystem.Application.Feature.Product.Commands.Request
{
    public record UpdateRequest(int id, ProductCommand Command) : IRequest<ProductQuery>;
}
