namespace FraudDetectionCsharp.Infra.messaging
{
    public class KafkaSettings
    {
        public string BootstrapServers { get; set; }
        public string TransactionTopic { get; set; }
        public string PaymentTopic { get; set; }
        public string AccountTopic { get; set; }

    }

}
