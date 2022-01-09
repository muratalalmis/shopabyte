﻿namespace Catalog.API.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Currency { get; set; }
        public decimal Price { get; set; }
    }
}
