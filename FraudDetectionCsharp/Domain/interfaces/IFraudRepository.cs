using FraudDetectionCsharp.Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraudDetectionCsharp.Domain.interfaces
{
    public interface IFraudRepository
    {
        Task SaveFraudAsync(Fraud fraud);  // Método para salvar detalhes da fraude no banco de dados
    }
}
