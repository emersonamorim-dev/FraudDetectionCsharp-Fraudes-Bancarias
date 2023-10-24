using Confluent.Kafka;
using FraudDetectionCsharp.Domain.entities;
using FraudDetectionCsharp.Domain.interfaces;
using FraudDetectionCsharp.Infra.messaging;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace FraudDetectionCsharp.Domain.services
{
    public class TransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly Producer _producer;
        private readonly IAccountRepository _accountRepository;

        public TransactionService(ITransactionRepository transactionRepository, Producer producer, IAccountRepository accountRepository)
        {
            _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
            _producer = producer ?? throw new ArgumentNullException(nameof(producer));
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        }

        public async Task CreateTransactionAsync(Transaction transaction)
        {
            // Save the transaction to the database
            await _transactionRepository.CreateTransactionAsync(transaction);

            // Publish a message to Kafka
            await PublishTransactionToKafka(transaction);
        }


        private async Task PublishTransactionToKafka(Transaction transaction)
        {
            try
            {
                var messageContent = JsonSerializer.Serialize(transaction);
                string topicName = $"Transaction-{transaction.Id}"; 

                await _producer.SendMessageAsync(topicName, messageContent);

                Console.WriteLine($"Mensagem de transação com ID {transaction.Id} enviado para o tópico Kafka {topicName}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unexpected error: {e.Message}");
            }
        }

    }
}
