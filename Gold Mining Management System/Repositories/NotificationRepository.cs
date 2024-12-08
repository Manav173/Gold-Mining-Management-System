using Gold_Mining_Management_System.Models;
using Gold_Mining_Management_System.Data;
using Microsoft.EntityFrameworkCore;

namespace Gold_Mining_Management_System.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly AppDbContext _context;

        public NotificationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Notifications>> GetAllNotificationsAsync()
        {
            return await _context.Notifications.Include(n => n.Receiver).Include(n => n.Sender).ToListAsync();
        }

        public async Task<Notifications> GetNotificationByIdAsync(int id)
        {
            return await _context.Notifications
                .Include(n => n.Receiver)
                .Include(n => n.Sender)
                .FirstOrDefaultAsync(n => n.NotificationId == id);
        }

        public async Task CreateNotificationAsync(Notifications notification)
        {
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateNotificationAsync(Notifications notification)
        {
            _context.Notifications.Update(notification);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteNotificationAsync(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification != null)
            {
                _context.Notifications.Remove(notification);
                await _context.SaveChangesAsync();
            }
        }
    }
}
