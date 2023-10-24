using FraudDetectionCsharp.Domain.entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FraudDetectionCsharp.Domain.interfaces
{
    public interface IFraudReportRepository
    {
        Task AddFraudReportAsync(FraudReport report);
        Task<FraudReport> GetFraudReportByIdAsync(int id);
        Task<IEnumerable<FraudReport>> GetFraudReportsAsync();
        Task UpdateFraudReportAsync(FraudReport report);
        Task DeleteFraudReportAsync(int id);
    }
}
