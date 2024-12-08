using Gold_Mining_Management_System.Models;
using Gold_Mining_Management_System.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Gold_Mining_Management_System.Services
{
    public interface IUserService
    {
        public Task<bool> RegisterUser(Users user);
        public Task<string> AuthenticateUser(Users user);
        public Task<ActionResult<UserCounts>> GetUserCountsAsync();
    }
}
