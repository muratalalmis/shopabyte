using FluentValidation;

namespace Ordering.Application.Features.Sales.Commands.PlaceOrder
{
    public class PlaceOrderCommandValidator : AbstractValidator<PlaceOrderCommand>
    {
        public PlaceOrderCommandValidator()
        {
            RuleFor(x => x.Currency)
                .NotNull()
                .Empty()
                .MaximumLength(3);

            RuleFor(x => x.CustomerId)
                .NotNull()
                .Empty();

            RuleFor(x => x.Total)
                .NotNull()
                .Empty();

            RuleFor(x => x.Items)
                .NotNull()
                .Empty();
        }
    }
}
