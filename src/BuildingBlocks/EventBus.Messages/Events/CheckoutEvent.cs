namespace EventBus.Messages.Events
{
    public class CheckoutEvent
    {
        public int CustomerId { get; set; }
        public string Currency { get; set; }
        public decimal Total { get; set; }
        public List<CheckOutItem> Items { get; set; }
    }
}
