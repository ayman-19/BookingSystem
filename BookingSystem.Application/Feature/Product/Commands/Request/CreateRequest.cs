using BookingSystem.DTOs.Product;
using MediatR;

namespace BookingSystem.Application.Feature.Product.Commands.Request
{
    public record CreateRequest(ProductCommand Command) : IRequest<ProductQuery>;
}
