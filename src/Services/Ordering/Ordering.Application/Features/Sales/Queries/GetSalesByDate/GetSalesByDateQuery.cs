using MediatR;

namespace Ordering.Application.Features.Sales.Queries.GetSalesByDate
{
    public class GetSalesByDateQuery : IRequest<List<SalesDto>>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
