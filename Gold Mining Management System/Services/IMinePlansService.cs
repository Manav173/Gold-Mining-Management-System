using Gold_Mining_Management_System.DTO;
using Gold_Mining_Management_System.Models;

namespace Gold_Mining_Management_System.Services
{
    public interface IMinePlansService
    {
        public Task<IEnumerable<MinePlans>> GetAllMinePlansAsync();
        public Task<MinePlans> GetMinePlanByIdAsync(int id);
        public Task AddMinePlanAsync(MinePlans minePlan);
        public Task UpdateMinePlanAsync(MinePlans minePlan);
        public Task DeleteMinePlanAsync(int id);
        public Task<MinePlanCounts> GetMinePlanCountsAsync();
    }
}
