using Gold_Mining_Management_System.DTO;
using Gold_Mining_Management_System.Models;
using Gold_Mining_Management_System.Repositories;

namespace Gold_Mining_Management_System.Services
{
    public class CostManagementService : ICostManagementService
    {
        private readonly ICostManagementRepository _repository;

        public CostManagementService(ICostManagementRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CostManagement>> GetAllCostsAsync()
        {
            return await _repository.GetAllCostsAsync();
        }

        public async Task<CostManagement> GetCostByIdAsync(int id)
        {
            return await _repository.GetCostByIdAsync(id);
        }

        public async Task AddCostAsync(CostManagement cost)
        {
            await _repository.AddCostAsync(cost);
        }

        public async Task UpdateCostAsync(CostManagement cost)
        {
            await _repository.UpdateCostAsync(cost);
        }

        public async Task DeleteCostAsync(int id)
        {
            await _repository.DeleteCostAsync(id);
        }

        public async Task<CostManagementCounts> GetCostManagementCountsAsync()
        {
            return await _repository.GetCostManagementCountsAsync();
        }
    }
}
