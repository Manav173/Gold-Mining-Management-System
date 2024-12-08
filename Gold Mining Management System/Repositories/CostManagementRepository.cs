using Gold_Mining_Management_System.Models;
using Gold_Mining_Management_System.Data;
using Microsoft.EntityFrameworkCore;
using Gold_Mining_Management_System.DTO;

namespace Gold_Mining_Management_System.Repositories
{
    public class CostManagementRepository : ICostManagementRepository
    {
        private readonly AppDbContext _context;

        public CostManagementRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CostManagement>> GetAllCostsAsync()
        {
            return await _context.CostManagement.Include(c => c.Manager).Include(p => p.Site).ToListAsync();
        }

        public async Task<CostManagement> GetCostByIdAsync(int id)
        {
            return await _context.CostManagement.Include(c => c.Manager).Include(p => p.Site).FirstOrDefaultAsync(c => c.CostId == id);
        }

        public async Task AddCostAsync(CostManagement cost)
        {
            await _context.CostManagement.AddAsync(cost);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCostAsync(CostManagement cost)
        {
            _context.CostManagement.Update(cost);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCostAsync(int id)
        {
            var cost = await _context.CostManagement.FindAsync(id);
            if (cost != null)
            {
                _context.CostManagement.Remove(cost);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<CostManagementCounts> GetCostManagementCountsAsync()
        {
            var totalCostData = await _context.CostManagement.CountAsync();
            var totalAmount = await _context.CostManagement.SumAsync(cm => cm.Amount);

            return new CostManagementCounts
            {
                TotalCostData = totalCostData,
                TotalAmount = totalAmount 
            };
        }
    }
}
