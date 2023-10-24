using FraudDetectionCsharp.Domain.entities;
using FraudDetectionCsharp.Domain.interfaces;
using FraudDetectionCsharp.Infra.messaging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace FraudDetectionCsharp.Domain.services
{
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly Producer _producer;


        public AccountService(IAccountRepository accountRepository, Producer producer)
        {
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            _producer = producer ?? throw new ArgumentNullException(nameof(producer));
        }

             // Salva a conta no Banco de dados
        public async Task CreateAccountAsync(Account account)
        {
            await _accountRepository.CreateAccountAsync(account);

            // Publica a mensagem no Kafka
            await PublishAccountToKafka(account);
        }

            //Listar todas Contas
        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            
            return await _accountRepository.GetAllAccountsAsync();
        }

            //Listar todas Contas por ID
        public async Task<Account> GetAccountByIdAsync(int id)
        {
            return await _accountRepository.GetAccountByIdAsync(id);
        }

           //Deletar todas Contas por ID
        public async Task DeleteAccountAsync(int id)
        {
            await _accountRepository.DeleteAccountAsync(id);
        }

        private async Task PublishAccountToKafka(Account account)
        {
            try
            {
                var messageContent = JsonSerializer.Serialize(account);
                string topicName = $"Account-{account.Id}";

                await _producer.SendMessageAsync(topicName, messageContent);

                Console.WriteLine($"Mensagem de transação com ID {account.Id} enviado para o tópico Kafka {topicName}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unexpected error: {e.Message}");
            }
        }

    }
}

