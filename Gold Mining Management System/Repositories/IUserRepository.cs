using Gold_Mining_Management_System.Models;

namespace Gold_Mining_Management_System.Repositories
{
    public interface IUserRepository
    {
        public Task AddUser(Users user);
        public Task<Users> GetUserByEmail(string email);
        public Task<Users?> GetUserById(int? id);
        public Task<bool> UpdateUser(int id, Users updatedUser);
        public Task<bool> DeleteUser(int id);
        public Task<int> GetTotalUsersCountAsync();
        public Task<int> GetActiveUsersCountAsync();
        public Task<int> GetAdminUsersCountAsync();
        public Task<int> GetMineManagersCountAsync();
        public Task<int> GetGeologistsCountAsync();
        public Task<int> GetEngineersCountAsync();
        public Task<int> GetSafetyOfficersCountAsync();
        public Task<int> GetFieldWorkersCountAsync();
    }
}
