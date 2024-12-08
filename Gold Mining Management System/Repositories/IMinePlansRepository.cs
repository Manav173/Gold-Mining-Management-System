using Gold_Mining_Management_System.Models;

namespace Gold_Mining_Management_System.Repositories
{
    public interface IMinePlansRepository
    {
        public Task<IEnumerable<MinePlans>> GetAllMinePlansAsync();
        public Task<MinePlans> GetMinePlanByIdAsync(int id);
        public Task AddMinePlanAsync(MinePlans minePlan);
        public Task UpdateMinePlanAsync(MinePlans minePlan);
        public Task DeleteMinePlanAsync(int id);
        public Task<int> GetTotalMinesAsync();
        public Task<int> GetTotalPlannedAsync();
        public Task<int> GetTotalInProgressAsync();
        public Task<int> GetTotalCompletedAsync();
    }
}
