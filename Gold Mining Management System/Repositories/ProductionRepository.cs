using Gold_Mining_Management_System.Models;
using Gold_Mining_Management_System.Data;
using Microsoft.EntityFrameworkCore;
using Gold_Mining_Management_System.DTO;

namespace Gold_Mining_Management_System.Repositories
{
    public class ProductionRepository : IProductionRepository
    {
        private readonly AppDbContext _context;

        public ProductionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Production>> GetAllProductionsAsync()
        {
            return await _context.Productions.Include(p => p.Site).Include(p => p.Supervisor).ToListAsync();
        }

        public async Task<Production> GetProductionByIdAsync(int productionId)
        {
            return await _context.Productions.Include(p => p.Site)
                                             .Include(p => p.Supervisor)      
                                             .FirstOrDefaultAsync(p => p.ProductionId == productionId);
        }

        public async Task AddProductionAsync(Production production)
        {
            _context.Productions.Add(production);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductionAsync(Production production)
        {
            _context.Productions.Update(production);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductionAsync(int productionId)
        {
            var production = await _context.Productions.FindAsync(productionId);
            if (production != null)
            {
                _context.Productions.Remove(production);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<object>> GetDailyProductionAsync()
        {
            return await _context.Set<Production>()
                .GroupBy(p => new { p.SiteId, Date = p.Date.Date })
                .Select(g => new
                {
                    SiteId = g.Key.SiteId,
                    Date = g.Key.Date,
                    TotalOreExtracted = g.Sum(p => p.OreExtracted),
                    TotalGoldProduced = g.Sum(p => p.GoldProduced)
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<object>> GetWeeklyProductionAsync()
        {
            return await _context.Set<Production>()
                .GroupBy(p => new
                {
                    p.SiteId,
                    Week = EF.Functions.DateDiffWeek(new DateTime(1900, 1, 1), p.Date) // Using DateDiff for weekly grouping
                })
                .Select(g => new
                {
                    SiteId = g.Key.SiteId,
                    WeekNumber = g.Key.Week,
                    TotalOreExtracted = g.Sum(p => p.OreExtracted),
                    TotalGoldProduced = g.Sum(p => p.GoldProduced)
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<object>> GetMonthlyProductionAsync()
        {
            return await _context.Set<Production>()
                .GroupBy(p => new { p.SiteId, Year = p.Date.Year, Month = p.Date.Month })
                .Select(g => new
                {
                    SiteId = g.Key.SiteId,
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalOreExtracted = g.Sum(p => p.OreExtracted),
                    TotalGoldProduced = g.Sum(p => p.GoldProduced)
                })
                .ToListAsync();
        }

        public async Task<ProductionCounts> GetProductionCountsAsync()
        {
            var totalProductionData = await _context.Productions.CountAsync();
            var totalOreExtracted = await _context.Productions.SumAsync(p => p.OreExtracted);
            var totalGoldProduced = await _context.Productions.SumAsync(p => p.GoldProduced);

            return new ProductionCounts
            {
                TotalProductionData = totalProductionData,
                TotalOreExtracted = totalOreExtracted,
                TotalGoldProduced = totalGoldProduced
            };
        }
    }
}
