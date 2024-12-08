using Gold_Mining_Management_System.Data;
using Gold_Mining_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Gold_Mining_Management_System.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddUser(Users user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<Users> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Users?> GetUserById(int? id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<bool> UpdateUser(int id, Users updatedUser)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);

            if (existingUser == null)
            {
                return false; 
            }

            existingUser.Username = updatedUser.Username;
            existingUser.Email = updatedUser.Email;
            existingUser.PasswordHash = updatedUser.PasswordHash;
            existingUser.Role = updatedUser.Role;
            existingUser.IsActive = updatedUser.IsActive;
 
            await _context.SaveChangesAsync();

            return true; 
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return false; // User not found
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true; // User successfully deleted
        }

        public async Task<int> GetTotalUsersCountAsync()
        {
            return await _context.Users.CountAsync();
        }

        public async Task<int> GetActiveUsersCountAsync()
        {
            return await _context.Users.CountAsync(u => u.IsActive == true);
        }

        public async Task<int> GetAdminUsersCountAsync()
        {
            return await _context.Users.CountAsync(u => u.Role == "Admin");
        }

        public async Task<int> GetMineManagersCountAsync()
        {
            return await _context.Users.CountAsync(u => u.Role == "Mine Manager");
        }

        public async Task<int> GetGeologistsCountAsync()
        {
            return await _context.Users.CountAsync(u => u.Role == "Geologist");
        }

        public async Task<int> GetEngineersCountAsync()
        {
            return await _context.Users.CountAsync(u => u.Role == "Engineer");
        }

        public async Task<int> GetSafetyOfficersCountAsync()
        {
            return await _context.Users.CountAsync(u => u.Role == "Safety Officer");
        }

        public async Task<int> GetFieldWorkersCountAsync()
        {
            return await _context.Users.CountAsync(u => u.Role == "Field Worker");
        }
    }
}
