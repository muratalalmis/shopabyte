namespace Ordering.Application.Contracts.Services
{
    public interface IOrderIdentityFactory
    {
        string GetIdentity(int memberId);
    }
}
