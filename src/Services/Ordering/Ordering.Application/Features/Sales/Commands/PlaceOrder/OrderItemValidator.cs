using FluentValidation;

namespace Ordering.Application.Features.Sales.Commands.PlaceOrder
{
    public class OrderItemValidator : AbstractValidator<OrderItem>
    {
        public OrderItemValidator()
        {
            RuleFor(x => x.ProductId)
                .NotNull()
                .Empty();

            RuleFor(x => x.Quantity)
              .NotNull()
              .Empty();

            RuleFor(x => x.Price)
              .NotNull()
              .Empty();
        }
    }
}
