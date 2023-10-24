using System.Threading.Tasks;
using FraudDetectionCsharp.Domain.entities;

namespace FraudDetectionCsharp.Domain.interfaces
{
    public interface IPaymentRepository
    {
        Task CreatePaymentAsync(Payment payment);
    }
}
