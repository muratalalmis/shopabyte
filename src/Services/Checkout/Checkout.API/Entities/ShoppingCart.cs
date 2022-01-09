namespace Checkout.API.Entities
{
    public class ShoppingCart
    {
        public int CustomerId { get; set; }
        public string Currency { get; set; }
        public decimal Total { get; set; }
        public List<ShoppingCartItem> Items { get; set; }
    }
}
