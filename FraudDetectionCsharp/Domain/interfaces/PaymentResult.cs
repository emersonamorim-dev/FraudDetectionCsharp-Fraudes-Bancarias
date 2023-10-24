namespace FraudDetectionCsharp.Domain.interfaces
{
    public class PaymentResult
    {
        public bool IsSuccess { get; set; }
        public int TransactionId { get; set; }  
        public string PaymentConfirmationNumber { get; set; }

        public string Status { get; set; }
        public string ErrorMessage { get; set; }
    }
}
