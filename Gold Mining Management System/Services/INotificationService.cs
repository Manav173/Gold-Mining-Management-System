using Gold_Mining_Management_System.Models;

namespace Gold_Mining_Management_System.Services
{
    public interface INotificationService
    {
        public Task<List<Notifications>> GetAllNotificationsAsync();
        public Task<Notifications> GetNotificationByIdAsync(int id);
        public Task CreateNotificationAsync(Notifications notification);
        public Task UpdateNotificationAsync(Notifications notification);
        public Task DeleteNotificationAsync(int id);
    }
}
