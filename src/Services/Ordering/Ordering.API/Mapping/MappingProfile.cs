using AutoMapper;
using EventBus.Messages.Events;
using Ordering.Application.Features.Sales.Commands.PlaceOrder;

namespace Ordering.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CheckoutEvent, PlaceOrderCommand>();
            CreateMap<CheckOutItem, OrderItem>();
        }
    }
}
