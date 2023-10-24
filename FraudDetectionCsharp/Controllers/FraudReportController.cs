using FraudDetectionCsharp.Domain.entities;
using FraudDetectionCsharp.Domain.services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FraudDetection.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FraudReportController : ControllerBase
    {
        private readonly FraudReportService _fraudReportService;

        public FraudReportController(FraudReportService fraudReportService)
        {
            _fraudReportService = fraudReportService ?? throw new ArgumentNullException(nameof(fraudReportService));
        }

        [HttpGet]
        public async Task<IEnumerable<FraudReport>> GetFraudReports()
        {
            return await _fraudReportService.GetFraudReportsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FraudReport>> GetFraudReport(int id)
        {
            var report = await _fraudReportService.GetFraudReportByIdAsync(id);

            if (report == null)
            {
                return NotFound();
            }

            return report;
        }

        [HttpPost]
        public async Task<ActionResult<FraudReport>> AddFraudReport(FraudReport report)
        {
            await _fraudReportService.AddFraudReportAsync(report);

            return CreatedAtAction(nameof(GetFraudReport), new { id = report.Id }, report);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFraudReport(int id, FraudReport report)
        {
            if (id != report.Id)
            {
                return BadRequest();
            }

            await _fraudReportService.UpdateFraudReportAsync(report);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFraudReport(int id)
        {
            await _fraudReportService.DeleteFraudReportAsync(id);

            return NoContent();
        }
    }
}
