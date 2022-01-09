using MediatR;

namespace Ordering.Application.Features.Sales.Commands.PlaceOrder
{
    /// <summary>
    /// Creates a new sales
    /// </summary>
    public class PlaceOrderCommand : IRequest<string>
    {
        public int CustomerId { get; set; }
        public string Currency { get; set; }
        public decimal Total { get; set; }
        public OrderItem[] Items { get; set; }
    }
}
