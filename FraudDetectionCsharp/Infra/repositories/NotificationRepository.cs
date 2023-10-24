using FraudDetectionCsharp.Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FraudDetectionCsharp.Infra.repositories
{
    public class NotificationRepository
    {
        private readonly List<Notification> _notifications;

        public NotificationRepository()
        {
            _notifications = new List<Notification>();
        }

        public async Task AddNotificationAsync(Notification notification)
        {
            if (notification == null)
            {
                throw new ArgumentNullException(nameof(notification));
            }

            // Simulando uma operação assíncrona
            await Task.Delay(100);

            _notifications.Add(notification);
        }

        public async Task<IEnumerable<Notification>> GetNotificationsForUserAsync(int userId)
        {
            // Simulando uma operação assíncrona
            await Task.Delay(100);

            return _notifications.Where(n => n.UserId == userId).ToList();
        }

        public async Task DeleteNotificationAsync(int notificationId)
        {
            // Simulando uma operação assíncrona
            await Task.Delay(100);

            var notification = _notifications.FirstOrDefault(n => n.Id == notificationId);
            if (notification != null)
            {
                _notifications.Remove(notification);
            }
        }
    }
}
