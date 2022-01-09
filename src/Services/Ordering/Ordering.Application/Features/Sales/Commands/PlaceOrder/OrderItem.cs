﻿namespace Ordering.Application.Features.Sales.Commands.PlaceOrder
{
    public class OrderItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
