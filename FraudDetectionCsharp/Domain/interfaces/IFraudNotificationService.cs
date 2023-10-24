using FraudDetectionCsharp.Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FraudDetectionCsharp.Domain.interfaces
{
    public interface IFraudNotificationService
    {
        Task NotifyFraudAsync(Fraud fraud);
    }
}
