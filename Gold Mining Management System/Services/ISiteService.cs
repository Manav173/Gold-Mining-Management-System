using Gold_Mining_Management_System.DTO;
using Gold_Mining_Management_System.Models;

namespace Gold_Mining_Management_System.Services
{
    public interface ISiteService
    {
        public Task<IEnumerable<Sites>> GetAllSitesAsync();
        public Task<Sites> GetSiteByIdAsync(int siteId);
        public Task AddSiteAsync(Sites site);
        public Task UpdateSiteAsync(Sites site);
        public Task DeleteSiteAsync(int siteId);
        public Task<SiteCounts> GetSiteCountsAsync();
    }
}
