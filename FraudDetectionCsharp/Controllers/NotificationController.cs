using FraudDetectionCsharp.Domain.entities;
using FraudDetectionCsharp.Domain.services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FraudDetectionCsharp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly NotificationService _notificationService;

        public NotificationController(NotificationService notificationService)
        {
            _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
        }

        [HttpPost]
        public async Task<IActionResult> AddNotification([FromBody] Notification notification)
        {
            if (notification == null)
            {
                return BadRequest("Notification cannot be null");
            }

            await _notificationService.AddNotificationAsync(notification);
            return Ok("Notification added successfully");
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetNotificationsForUser(int userId)
        {
            if (userId <= 0)
            {
                return BadRequest("Invalid user ID");
            }

            var notifications = await _notificationService.GetNotificationsForUserAsync(userId);
            return Ok(notifications);
        }

        [HttpDelete("{notificationId}")]
        public async Task<IActionResult> DeleteNotification(int notificationId)
        {
            if (notificationId <= 0)
            {
                return BadRequest("Invalid notification ID");
            }

            await _notificationService.DeleteNotificationAsync(notificationId);
            return Ok("Notification deleted successfully");
        }
    }
}
