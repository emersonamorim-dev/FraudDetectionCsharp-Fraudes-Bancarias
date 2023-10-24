using FraudDetectionCsharp.Domain.entities;
using FraudDetectionCsharp.Infra.repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FraudDetectionCsharp.Domain.services
{
    public class NotificationService
    {
        private readonly NotificationRepository _notificationRepository;

        public NotificationService(NotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository ?? throw new ArgumentNullException(nameof(notificationRepository));
        }

        public async Task AddNotificationAsync(Notification notification)
        {
            if (notification == null)
            {
                throw new ArgumentNullException(nameof(notification));
            }

            await _notificationRepository.AddNotificationAsync(notification);
        }

        public async Task<IEnumerable<Notification>> GetNotificationsForUserAsync(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("User ID must be positive.", nameof(userId));
            }

            return await _notificationRepository.GetNotificationsForUserAsync(userId);
        }


        public async Task NotifyUserAsync(int userId, string message)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("User ID must be positive.", nameof(userId));
            }

            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException(nameof(message));
            }

            var notification = new Notification
            {
                UserId = userId,
                Message = message,
                DateSent = DateTime.Now  
            };

            await _notificationRepository.AddNotificationAsync(notification);
        }


        public async Task DeleteNotificationAsync(int notificationId)
        {
            if (notificationId <= 0)
            {
                throw new ArgumentException("Notification ID must be positive.", nameof(notificationId));
            }

            await _notificationRepository.DeleteNotificationAsync(notificationId);
        }


    }
}
