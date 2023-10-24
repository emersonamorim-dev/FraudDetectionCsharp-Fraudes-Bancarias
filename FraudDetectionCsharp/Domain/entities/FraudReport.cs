using System;

namespace FraudDetectionCsharp.Domain.entities
{
    public class FraudReport
    {
        public int Id { get; set; } 
        public int AccountId { get; set; } 
        public string Description { get; set; } 
        public DateTime ReportedDate { get; set; } 
        public FraudStatus Status { get; set; } 



    }
}
