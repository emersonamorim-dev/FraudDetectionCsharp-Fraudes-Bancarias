using System.Threading.Tasks;
using FraudDetectionCsharp.Domain.entities;

namespace FraudDetectionCsharp.Domain.interfaces
{
    public interface IPaymentService
    {
        Task ProcessPaymentAsync(Payment payment);
    }
}
