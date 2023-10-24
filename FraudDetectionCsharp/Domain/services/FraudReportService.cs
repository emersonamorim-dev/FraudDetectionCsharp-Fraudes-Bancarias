using FraudDetectionCsharp.Domain.entities;
using FraudDetectionCsharp.Infra.repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FraudDetectionCsharp.Domain.services
{
    public class FraudReportService
    {
        private readonly FraudReportRepository _fraudReportRepository;

        public FraudReportService(FraudReportRepository fraudReportRepository)
        {
            _fraudReportRepository = fraudReportRepository ?? throw new ArgumentNullException(nameof(fraudReportRepository));
        }

        public async Task AddFraudReportAsync(FraudReport report)
        {
            if (report == null)
            {
                throw new ArgumentNullException(nameof(report));
            }

            await _fraudReportRepository.AddFraudReportAsync(report);
        }

        public async Task<IEnumerable<FraudReport>> GetFraudReportsAsync()
        {
            return await _fraudReportRepository.GetFraudReportsAsync();
        }
        public async Task<FraudReport> GetFraudReportByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid report ID", nameof(id));
            }

            return await _fraudReportRepository.GetFraudReportByIdAsync(id);
        }

        public async Task UpdateFraudReportAsync(FraudReport report)
        {
            if (report == null)
            {
                throw new ArgumentNullException(nameof(report));
            }

            if (report.Id <= 0)
            {
                throw new ArgumentException("Invalid report ID", nameof(report.Id));
            }

            await _fraudReportRepository.UpdateFraudReportAsync(report);
        }

        public async Task DeleteFraudReportAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid report ID", nameof(id));
            }

            await _fraudReportRepository.DeleteFraudReportAsync(id);
        }
    }
}
