using FraudDetectionCsharp.Domain.entities;
using FraudDetectionCsharp.Domain.interfaces;
using FraudDetectionCsharp.Infra.messaging;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace FraudDetectionCsharp.Domain.services
{
    public class PaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly Producer _producer;


        public PaymentService(IPaymentRepository paymentRepository, Producer producer)
        {
            _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
            _producer = producer ?? throw new ArgumentNullException(nameof(producer));
        }

        public async Task CreatePaymentAsync(Payment payment)
        {

            // Salva o pagamento no Banco de dados
            await _paymentRepository.CreatePaymentAsync(payment);

            // Publica a mensagem no Kafka
            await PublishPaymentToKafka(payment);
        }

        private async Task PublishPaymentToKafka(Payment payment)
        {
            try
            {
                var messageContent = JsonSerializer.Serialize(payment);
                string topicName = $"Payment-{payment.Id}";

                await _producer.SendMessageAsync(topicName, messageContent);

                Console.WriteLine($"Mensagem de Pagamento com ID {payment.Id} enviado para o tópico Kafka {topicName}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unexpected error: {e.Message}");
            }
        }
    }
}
