namespace Ordering.Application.Features.Sales.Queries.GetSalesByDate
{
    public class SalesDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string DocNo { get; set; }
        public DateTime DocDate { get; set; }
        public string Currency { get; set; }
        public decimal Total { get; set; }
        public IReadOnlyList<SalesItemDto> Items { get; set; }
    }
}
