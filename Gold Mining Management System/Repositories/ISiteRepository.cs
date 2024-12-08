using Gold_Mining_Management_System.Models;

namespace Gold_Mining_Management_System.Repositories
{
    public interface ISiteRepository
    {
        public Task<IEnumerable<Sites>> GetAllAsync();
        public Task<Sites> GetByIdAsync(int siteId);
        public Task AddAsync(Sites site);
        public Task UpdateAsync(Sites site);
        public Task DeleteAsync(int siteId);
        public Task<int> GetTotalSitesAsync();
        public Task<int> GetActiveSitesAsync();
        public Task<double> GetTotalAreaAsync();
        public Task<double> GetTotalYieldEstimateAsync();
    }
}
