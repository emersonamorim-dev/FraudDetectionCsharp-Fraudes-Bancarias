using FraudDetectionCsharp.Domain.entities;

namespace FraudDetectionCsharp.Domain.interfaces
{
    public interface IAccountService
    {
        Task<Account> CreateAccountAsync(Account account);

    }
}
