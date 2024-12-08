using Gold_Mining_Management_System.Models;
using Gold_Mining_Management_System.Repositories;

namespace Gold_Mining_Management_System.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<List<Notifications>> GetAllNotificationsAsync()
        {
            return await _notificationRepository.GetAllNotificationsAsync();
        }

        public async Task<Notifications> GetNotificationByIdAsync(int id)
        {
            return await _notificationRepository.GetNotificationByIdAsync(id);
        }

        public async Task CreateNotificationAsync(Notifications notification)
        {
            await _notificationRepository.CreateNotificationAsync(notification);
        }

        public async Task UpdateNotificationAsync(Notifications notification)
        {
            await _notificationRepository.UpdateNotificationAsync(notification);
        }

        public async Task DeleteNotificationAsync(int id)
        {
            await _notificationRepository.DeleteNotificationAsync(id);
        }
    }
}
