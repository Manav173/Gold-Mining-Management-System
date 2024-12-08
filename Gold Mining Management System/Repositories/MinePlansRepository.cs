using Gold_Mining_Management_System.Models;
using Gold_Mining_Management_System.Data;
using Microsoft.EntityFrameworkCore;

namespace Gold_Mining_Management_System.Repositories
{
    public class MinePlansRepository : IMinePlansRepository
    {
        private readonly AppDbContext _context;

        public MinePlansRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MinePlans>> GetAllMinePlansAsync()
        {
            return await _context.MinePlans.Include(m => m.Engineer).Include(m => m.Site).ToListAsync();
        }

        public async Task<MinePlans> GetMinePlanByIdAsync(int id)
        {
            return await _context.MinePlans
                .Include(m => m.Engineer)
                .Include(m => m.Site)
                .FirstOrDefaultAsync(m => m.PlanId == id);
        }

        public async Task AddMinePlanAsync(MinePlans minePlan)
        {
            _context.MinePlans.Add(minePlan);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMinePlanAsync(MinePlans minePlan)
        {
            _context.MinePlans.Update(minePlan);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMinePlanAsync(int id)
        {
            var minePlan = await _context.MinePlans.FindAsync(id);
            if (minePlan != null)
            {
                _context.MinePlans.Remove(minePlan);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> GetTotalMinesAsync()
        {
            return await _context.MinePlans.CountAsync();
        }

        public async Task<int> GetTotalPlannedAsync()
        {
            return await _context.MinePlans.CountAsync(mp => mp.Status == "Planned");
        }

        public async Task<int> GetTotalInProgressAsync()
        {
            return await _context.MinePlans.CountAsync(mp => mp.Status == "In Progress");
        }

        public async Task<int> GetTotalCompletedAsync()
        {
            return await _context.MinePlans.CountAsync(mp => mp.Status == "Completed");
        }
    }
}
