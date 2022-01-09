using AutoMapper;
using Checkout.API.Entities;
using EventBus.Messages.Events;

namespace Checkout.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ShoppingCart, CheckoutEvent>();
            CreateMap<ShoppingCartItem, CheckOutItem>();
        }
    }
}
