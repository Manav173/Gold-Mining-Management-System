using Gold_Mining_Management_System.Models;
using Gold_Mining_Management_System.Data;
using Microsoft.EntityFrameworkCore;

namespace Gold_Mining_Management_System.Repositories
{
    public class SiteRepository : ISiteRepository
    {
        private readonly AppDbContext _context;

        public SiteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Sites>> GetAllAsync()
        {
            return await _context.Sites.Include(s => s.Manager).ToListAsync();
        }

        public async Task<Sites> GetByIdAsync(int siteId)
        {
            return await _context.Sites.Include(s => s.Manager).FirstOrDefaultAsync(s => s.SiteId == siteId);
        }

        public async Task AddAsync(Sites site)
        {
            await _context.Sites.AddAsync(site);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Sites site)
        {
            _context.Sites.Update(site);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int siteId)
        {
            var site = await _context.Sites.FirstOrDefaultAsync(s => s.SiteId == siteId);
            if (site != null)
            {
                _context.Sites.Remove(site);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<int> GetTotalSitesAsync()
        {
            return await _context.Sites.CountAsync();
        }

        public async Task<int> GetActiveSitesAsync()
        {
            return await _context.Sites.CountAsync(site => site.Status == "Active"); 
        }

        public async Task<double> GetTotalAreaAsync()
        {
            return await _context.Sites.SumAsync(site => site.TotalArea);  
        }

        public async Task<double> GetTotalYieldEstimateAsync()
        {
            return await _context.Sites.SumAsync(site => site.YieldEstimate);  
        }
    }
}
