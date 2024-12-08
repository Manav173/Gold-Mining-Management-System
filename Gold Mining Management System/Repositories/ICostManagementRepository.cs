using Gold_Mining_Management_System.DTO;
using Gold_Mining_Management_System.Models;

namespace Gold_Mining_Management_System.Repositories
{
    public interface ICostManagementRepository
    {
        public Task<IEnumerable<CostManagement>> GetAllCostsAsync();
        public Task<CostManagement> GetCostByIdAsync(int id);
        public Task AddCostAsync(CostManagement cost);
        public Task UpdateCostAsync(CostManagement cost);
        public Task DeleteCostAsync(int id);
        public Task<CostManagementCounts> GetCostManagementCountsAsync();
    }
}
