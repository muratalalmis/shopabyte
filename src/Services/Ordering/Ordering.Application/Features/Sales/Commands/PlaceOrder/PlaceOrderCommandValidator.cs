using FluentValidation;

namespace Ordering.Application.Features.Sales.Commands.PlaceOrder
{
    public class PlaceOrderCommandValidator : AbstractValidator<PlaceOrderCommand>
    {
        public PlaceOrderCommandValidator()
        {
            RuleFor(x => x.Currency).MaximumLength(3);
        }
    }
}
