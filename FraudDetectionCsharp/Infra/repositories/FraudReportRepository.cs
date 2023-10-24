using FraudDetectionCsharp.Domain.entities;
using FraudDetectionCsharp.Domain.interfaces;
using FraudDetectionCsharp.Infra.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FraudDetectionCsharp.Infra.repositories
{
    public class FraudReportRepository : IFraudReportRepository
    {
        private readonly List<FraudReport> _fraudReports;
        private readonly ApplicationDbContext _context;

        public FraudReportRepository(ApplicationDbContext context)
        {
            _fraudReports = new List<FraudReport>();
            _context = context ?? throw new ArgumentNullException(nameof(context));

        }

        public async Task AddFraudReportAsync(FraudReport report)
        {
            if (report == null)
            {
                throw new ArgumentNullException(nameof(report));
            }

            // Simulando uma operação assíncrona
            await Task.Delay(100);

            _fraudReports.Add(report);
        }

        public async Task<IEnumerable<FraudReport>> GetFraudReportsAsync()
        {
            // Retorna à lista em memória.
            return _fraudReports.AsReadOnly();
        }


        public async Task<FraudReport> GetFraudReportByIdAsync(int id)
        {
            // Simulando uma operação assíncrona
            await Task.Delay(100);

            return _fraudReports.FirstOrDefault(r => r.Id == id);
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

            // Simulando uma operação assíncrona
            await Task.Delay(100);

            var existingReport = _fraudReports.FirstOrDefault(r => r.Id == report.Id);
            if (existingReport == null)
            {
                throw new InvalidOperationException($"Report with ID {report.Id} not found");
            }

            // Aqui você pode atualizar as propriedades do relatório
            existingReport.Description = report.Description;
            existingReport.ReportedDate = report.ReportedDate;
            existingReport.Status = report.Status;
        }

        public async Task DeleteFraudReportAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid report ID", nameof(id));
            }

            // Simulando uma operação assíncrona
            await Task.Delay(100);

            var report = _fraudReports.FirstOrDefault(r => r.Id == id);
            if (report == null)
            {
                throw new InvalidOperationException($"Report with ID {id} not found");
            }

            _fraudReports.Remove(report);
        }
    }
}
