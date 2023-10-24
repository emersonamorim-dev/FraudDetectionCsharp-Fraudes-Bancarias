using FraudDetectionCsharp.Domain.entities;

namespace FraudDetectionCsharp.Domain.interfaces
{
    public interface IGeoLocationService
    {
        Task<Location> GetLocationFromIPAsync(string ipAddress);
    }
}
