namespace Ordering.Domain.Entities
{
    public class Sales : IAuditableEntity
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string DocNo { get; set; }
        public DateTime DocDate { get; set; }
        public string Currency { get; set; }
        public decimal Total { get; set; }

        // Audit
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? LastModifiedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }

        public ICollection<SalesItem> Items { get; set; }
    }
}
