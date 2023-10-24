using Confluent.Kafka;
using FraudDetectionCsharp.Domain.entities;
using Microsoft.Extensions.Options;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace FraudDetectionCsharp.Infra.messaging
{
    public class FraudMessagePublisher
    {
        private readonly IProducer<Null, string> _producer;
        private readonly IOptions<KafkaSettings> _kafkaSettings;

        public FraudMessagePublisher(ProducerConfig config, IOptions<KafkaSettings> kafkaSettings)
        {
            _producer = new ProducerBuilder<Null, string>(config).Build();
            _kafkaSettings = kafkaSettings;
        }

        public async Task PublishFraudMessageAsync(Fraud fraud, string topicName)
        {
            try
            {
                Console.WriteLine($"Attempting to publish message to topic: {topicName}");

                if (string.IsNullOrEmpty(topicName))
                {
                    throw new ArgumentException("Topic name cannot be null or empty", nameof(topicName));
                }

                var message = new Message<Null, string>
                {
                    Value = JsonSerializer.Serialize(fraud)
                };

                await _producer.ProduceAsync(topicName, message);


                Console.WriteLine($"Message sent to topic {topicName}");
            }
            catch (ProduceException<Null, string> e)
            {
                Console.WriteLine($"Failed to deliver message to {topicName}: {e.Message} [{e.Error.Code}]");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unexpected error: {e.Message}");
            }
        }
    }
}
