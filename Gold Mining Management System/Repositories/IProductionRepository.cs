using Gold_Mining_Management_System.DTO;
using Gold_Mining_Management_System.Models;

namespace Gold_Mining_Management_System.Repositories
{
    public interface IProductionRepository
    {
        public Task<IEnumerable<Production>> GetAllProductionsAsync();
        public Task<Production> GetProductionByIdAsync(int productionId);
        public Task AddProductionAsync(Production production);
        public Task UpdateProductionAsync(Production production);
        public Task DeleteProductionAsync(int productionId);
        public Task<IEnumerable<object>> GetDailyProductionAsync();
        public Task<IEnumerable<object>> GetWeeklyProductionAsync();
        public Task<IEnumerable<object>> GetMonthlyProductionAsync();
        public Task<ProductionCounts> GetProductionCountsAsync();
    }
}
