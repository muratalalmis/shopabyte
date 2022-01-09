using System.Runtime.Serialization;

namespace Checkout.API.Exceptions
{
    [Serializable]
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(int productId)
            : base($"Product {productId} not found")
        {
            ProductId = productId;
        }

        protected ProductNotFoundException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
            ProductId = info.GetInt32("productId");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("productId", ProductId);
        }

        public int ProductId { get; protected set; }
    }
}
