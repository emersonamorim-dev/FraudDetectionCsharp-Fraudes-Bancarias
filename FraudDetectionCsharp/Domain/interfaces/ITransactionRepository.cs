using FraudDetectionCsharp.Domain.entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FraudDetectionCsharp.Domain.interfaces
{
    public interface ITransactionRepository
    {
        Task CreateTransactionAsync(Transaction transaction);
        Task<int> GetTransactionCountForUserOnDateAsync(int userId, DateTime date);
    }
}
