using Gold_Mining_Management_System.DTO;
using Gold_Mining_Management_System.Models;
using Gold_Mining_Management_System.Repositories;

namespace Gold_Mining_Management_System.Services
{
    public class MinePlansService : IMinePlansService
    {
        private readonly IMinePlansRepository _repository;

        public MinePlansService(IMinePlansRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<MinePlans>> GetAllMinePlansAsync()
        {
            return await _repository.GetAllMinePlansAsync();
        }

        public async Task<MinePlans> GetMinePlanByIdAsync(int id)
        {
            return await _repository.GetMinePlanByIdAsync(id);
        }

        public async Task AddMinePlanAsync(MinePlans minePlan)
        {
            await _repository.AddMinePlanAsync(minePlan);
        }

        public async Task UpdateMinePlanAsync(MinePlans minePlan)
        {
            await _repository.UpdateMinePlanAsync(minePlan);
        }

        public async Task DeleteMinePlanAsync(int id)
        {
            await _repository.DeleteMinePlanAsync(id);
        }

        public async Task<MinePlanCounts> GetMinePlanCountsAsync()
        {
            var totalMines = await _repository.GetTotalMinesAsync();
            var totalPlanned = await _repository.GetTotalPlannedAsync();
            var totalInProgress = await _repository.GetTotalInProgressAsync();
            var totalCompleted = await _repository.GetTotalCompletedAsync();

            return new MinePlanCounts
            {
                TotalMines = totalMines,
                TotalPlanned = totalPlanned,
                TotalInProgress = totalInProgress,
                TotalCompleted = totalCompleted
            };
        }
    }
}
