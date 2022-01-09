using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Models;

namespace Ordering.Infrastructure.Services
{
    public class DefaultEmailService : IEmailService
    {
        public Task<bool> SendEmail(Email email)
        {
            // TODO implement
            throw new NotImplementedException();
        }
    }
}
