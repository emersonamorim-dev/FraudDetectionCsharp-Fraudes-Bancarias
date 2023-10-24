using System;
using Confluent.Kafka;

namespace FraudDetectionCsharp.Infra.messaging
{
    public class Producer
    {
        private readonly KafkaSettings _kafkaSettings;
        private readonly ProducerConfig _producerConfig;

        public Producer(KafkaConfig kafkaConfig, KafkaSettings kafkaSettings)
        {
            _kafkaSettings = kafkaSettings;

            _producerConfig = new ProducerConfig
            {
                BootstrapServers = kafkaSettings.BootstrapServers ?? kafkaConfig.ConnectionString
            };
        }
        public async Task SendMessageAsync(string topic, string messageContent)
        {
            using (var producer = new ProducerBuilder<Null, string>(_producerConfig).Build())
            {
                try
                {
                    var message = new Message<Null, string> { Value = messageContent };
                    var deliveryResult = await producer.ProduceAsync(topic, message);

                    Console.WriteLine($"Mensagem '{deliveryResult.Value}' enviada ao tópico {deliveryResult.TopicPartitionOffset}");
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Erro ao enviar mensagem: {e.Error.Reason}");
                }
            }
        }

    }
}
