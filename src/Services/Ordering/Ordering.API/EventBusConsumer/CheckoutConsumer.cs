using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Ordering.Application.Features.Sales.Commands.PlaceOrder;

namespace Ordering.API.EventBusConsumer
{
    public class CheckoutConsumer : IConsumer<CheckoutEvent>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<CheckoutConsumer> _logger;

        public CheckoutConsumer(IMediator mediator, IMapper mapper, ILogger<CheckoutConsumer> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<CheckoutEvent> context)
        {
            var command = _mapper.Map<PlaceOrderCommand>(context.Message);
            var result = await _mediator.Send(command);

            _logger.LogInformation("BasketCheckoutEvent consumed successfully. Created Order Id : {newOrderId}", result);
        }
    }
}
