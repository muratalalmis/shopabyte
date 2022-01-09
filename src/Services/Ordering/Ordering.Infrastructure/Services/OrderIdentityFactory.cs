using Ordering.Application.Contracts.Services;

namespace Ordering.Infrastructure.Services
{
    public class OrderIdentityFactory : IOrderIdentityFactory
    {
        public string GetIdentity(int memberId)
        {
            return $"SLS00{memberId}001";
        }
    }
}
