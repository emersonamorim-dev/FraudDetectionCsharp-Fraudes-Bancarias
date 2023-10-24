namespace FraudDetectionCsharp.Domain.entities
{
    public class FraudReportCreateRequest
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Description { get; set; }
        public DateTime ReportedDate { get; set; }
        public int Status { get; set; }
    }

}
