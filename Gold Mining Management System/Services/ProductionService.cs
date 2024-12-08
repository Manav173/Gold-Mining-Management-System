using Gold_Mining_Management_System.DTO;
using Gold_Mining_Management_System.Models;
using Gold_Mining_Management_System.Repositories;

namespace Gold_Mining_Management_System.Services
{
    public class ProductionService : IProductionService
    {
        private readonly IProductionRepository _repository;

        public ProductionService(IProductionRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Production>> GetAllProductionsAsync()
        {
            return await _repository.GetAllProductionsAsync();
        }

        public async Task<Production> GetProductionByIdAsync(int productionId)
        {
            return await _repository.GetProductionByIdAsync(productionId);
        }

        public async Task AddProductionAsync(Production production)
        {
            await _repository.AddProductionAsync(production);
        }

        public async Task UpdateProductionAsync(Production production)
        {
            await _repository.UpdateProductionAsync(production);
        }

        public async Task DeleteProductionAsync(int productionId)
        {
            await _repository.DeleteProductionAsync(productionId);
        }

        public async Task<IEnumerable<object>> GetDailyProductionAsync()
        {
            return await _repository.GetDailyProductionAsync();
        }

        public async Task<IEnumerable<object>> GetWeeklyProductionAsync()
        {
            return await _repository.GetWeeklyProductionAsync();
        }

        public async Task<IEnumerable<object>> GetMonthlyProductionAsync()
        {
            return await _repository.GetMonthlyProductionAsync();
        }

        public async Task<ProductionCounts> GetProductionCountsAsync()
        {
            return await _repository.GetProductionCountsAsync();
        }
    }
}
