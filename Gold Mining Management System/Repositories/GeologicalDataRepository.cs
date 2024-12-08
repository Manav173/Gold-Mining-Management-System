using Gold_Mining_Management_System.Models;
using Gold_Mining_Management_System.Data;
using Microsoft.EntityFrameworkCore;
using Gold_Mining_Management_System.DTO;

namespace Gold_Mining_Management_System.Repositories
{
    public class GeologicalDataRepository : IGeologicalDataRepository
    {
        private readonly AppDbContext _context;

        public GeologicalDataRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GeologicalData>> GetAllGeologicalDataAsync()
        {
            return await _context.GeologicalData
                .Include(g => g.SurveyReport)
                .Include(g => g.Geologist)
                .Include(g => g.Site)
                .ToListAsync();
        }

        public async Task<GeologicalData> GetGeologicalDataByIdAsync(int id)
        {
            return await _context.GeologicalData
                .Include(g => g.SurveyReport)
                .Include(g => g.Geologist)
                .Include(g => g.Site)
                .FirstOrDefaultAsync(g => g.DataId == id);
        }

        public async Task<GeologicalData> GetGeologicalDataBySiteIdAsync(int id)
        {
            return await _context.GeologicalData
                .Include(g => g.SurveyReport)
                .Include(g => g.Geologist)
                .Include(g => g.Site)
                .FirstOrDefaultAsync(g => g.SiteId == id);
        }

        public async Task AddGeologicalDataAsync(GeologicalData geologicalData)
        {
            _context.GeologicalData.Add(geologicalData);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGeologicalDataAsync(GeologicalData geologicalData)
        {
            _context.GeologicalData.Update(geologicalData);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGeologicalDataAsync(int id)
        {
            var geologicalData = await _context.GeologicalData.FindAsync(id);
            if (geologicalData != null)
            {
                _context.GeologicalData.Remove(geologicalData);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<GeologicalDataCounts> FetchGeologicalDataCountsAsync()
        {
            var totalGeologists = await _context.Users.CountAsync(u => u.Role == "Geologist");
            var totalQuartz = await _context.GeologicalData.SumAsync(d => d.Quartz);
            var totalLimonite = await _context.GeologicalData.SumAsync(d => d.Limonite);
            var totalReports = await _context.GeologicalData.CountAsync(d => d.ReportId != null);

            return new GeologicalDataCounts
            {
                TotalGeologists = totalGeologists,
                TotalQuartz = totalQuartz,
                TotalLimonite = totalLimonite,
                TotalReports = totalReports
            };
        }
    }
}
