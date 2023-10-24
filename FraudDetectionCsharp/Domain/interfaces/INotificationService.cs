using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraudDetectionCsharp.Domain.interfaces
{
    public interface INotificationService
    {
        Task SendNotificationAsync(string message, string recipient);
    }
}