using AutoMapper;
using Ordering.Application.Features.Sales.Commands.PlaceOrder;
using Ordering.Application.Features.Sales.Queries.GetSalesByDate;
using Ordering.Domain.Entities;

namespace Ordering.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Sales, SalesDto>();
            CreateMap<SalesItem, SalesItemDto>();
            CreateMap<PlaceOrderCommand, Sales>();
            CreateMap<OrderItem, SalesItem>();
        }
    }
}