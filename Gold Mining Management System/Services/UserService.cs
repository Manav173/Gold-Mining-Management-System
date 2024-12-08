using Gold_Mining_Management_System.Models;
using Gold_Mining_Management_System.DTO;
using Gold_Mining_Management_System.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gold_Mining_Management_System.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordHasher<Users> _passwordHasher;
        private readonly JwtService _jwtService;

        public UserService(IUserRepository userRepository, JwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _passwordHasher = new PasswordHasher<Users>();
        }
        public async Task<bool> RegisterUser(Users user)
        {
            var u = await _userRepository.GetUserByEmail(user.Email);
            if (u != null) return false;
            user.PasswordHash = _passwordHasher.HashPassword(user, user.PasswordHash);
            await _userRepository.AddUser(user);
            return true;
        }

        public async Task<string> AuthenticateUser(Users u)
        {
            var user = await _userRepository.GetUserByEmail(u.Email);
            if (user == null) return null;
            if (user.Username == u.Username && user.Role == u.Role && user.IsActive == true)
            {
                var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, u.PasswordHash);
                if (result == PasswordVerificationResult.Failed) return null;
                return _jwtService.GenerateToken(user.Username, user.Role);
            }
            else
            {
                return null;
            }
        }

        public async Task<ActionResult<UserCounts>> GetUserCountsAsync()
        {
            var userCounts = new UserCounts
            {
                TotalUsers = await _userRepository.GetTotalUsersCountAsync(),
                ActiveUsers = await _userRepository.GetActiveUsersCountAsync(),
                AdminUsers = await _userRepository.GetAdminUsersCountAsync(),
                MineManagers = await _userRepository.GetMineManagersCountAsync(),
                Geologists = await _userRepository.GetGeologistsCountAsync(),
                Engineers = await _userRepository.GetEngineersCountAsync(),
                SafetyOfficers = await _userRepository.GetSafetyOfficersCountAsync(),
                FieldWorkers = await _userRepository.GetFieldWorkersCountAsync()
            };

            return userCounts;
        }
    }
}
