namespace Checkout.API.Entities
{
    public class ShoppingCartItem
    {
        public Guid Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
