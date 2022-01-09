using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Contracts.Services;
using Ordering.Application.Models;

namespace Ordering.Application.Features.Sales.Commands.PlaceOrder
{
    public class PlaceOrderCommandHandler : IRequestHandler<PlaceOrderCommand, string>
    {
        private readonly IMapper _mapper;
        private readonly ISalesRepository _salesRepository;
        private readonly IEmailService _emailService;
        private readonly IOrderIdentityFactory _orderIdentityFactory;
        private readonly ILogger<PlaceOrderCommandHandler> _logger;

        public PlaceOrderCommandHandler(
            IMapper mapper,
            ISalesRepository salesRepository,
            IEmailService emailService,
            IOrderIdentityFactory orderIdentityFactory,
            ILogger<PlaceOrderCommandHandler> logger)
        {
            _mapper = mapper;
            _salesRepository = salesRepository;
            _emailService = emailService;
            _orderIdentityFactory = orderIdentityFactory;
            _logger = logger;
        }

        public async Task<string> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
        {
            var sales = _mapper.Map<Domain.Entities.Sales>(request);
            sales.DocNo = _orderIdentityFactory.GetIdentity(request.CustomerId);
            var newSales = await _salesRepository.AddAsync(sales);

            _logger.LogInformation($"Order {newSales.DocNo} placed.");
            await SendMail(newSales);

            return newSales.DocNo;
        }

        private async Task SendMail(Domain.Entities.Sales sales)
        {
            var email = new Email();
            try
            {
                await _emailService.SendEmail(email);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Order {sales.DocNo} failed due to an error with the mail service: {ex.Message}");
            }
        }
    }
}
