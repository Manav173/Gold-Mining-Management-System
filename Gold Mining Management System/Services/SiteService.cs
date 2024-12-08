using Gold_Mining_Management_System.Models;
using Gold_Mining_Management_System.DTO;
using Gold_Mining_Management_System.Repositories;

namespace Gold_Mining_Management_System.Services
{
    public class SiteService : ISiteService
    {
        private readonly ISiteRepository _siteRepository;

        public SiteService(ISiteRepository siteRepository)
        {
            _siteRepository = siteRepository;
        }

        public async Task<IEnumerable<Sites>> GetAllSitesAsync()
        {
            return await _siteRepository.GetAllAsync();
        }

        public async Task<Sites> GetSiteByIdAsync(int siteId)
        {
            return await _siteRepository.GetByIdAsync(siteId);
        }

        public async Task AddSiteAsync(Sites site)
        {
            await _siteRepository.AddAsync(site);
        }

        public async Task UpdateSiteAsync(Sites site)
        {
            await _siteRepository.UpdateAsync(site);
        }

        public async Task DeleteSiteAsync(int siteId)
        {
            await _siteRepository.DeleteAsync(siteId);
        }

        public async Task<SiteCounts> GetSiteCountsAsync()
        {
            var totalSites = await _siteRepository.GetTotalSitesAsync();
            var activeSites = await _siteRepository.GetActiveSitesAsync();
            var totalArea = await _siteRepository.GetTotalAreaAsync();
            var totalYieldEstimate = await _siteRepository.GetTotalYieldEstimateAsync();

            return new SiteCounts
            {
                TotalSites = totalSites,
                ActiveSites = activeSites,
                TotalArea = totalArea,
                TotalYieldEstimate = totalYieldEstimate
            };
        }
    }
}
