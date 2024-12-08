using Gold_Mining_Management_System.Data;
using Gold_Mining_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Gold_Mining_Management_System.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly AppDbContext _context;

        public ReportRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reports>> GetAllReportsAsync()
        {
            return await _context.Reports.Include(r => r.Creator).ToListAsync();
        }

        public async Task<Reports> GetReportByIdAsync(int? id)
        {
            return await _context.Reports.Include(r => r.Creator).FirstOrDefaultAsync(r => r.ReportId == id);
        }

        public async Task AddReportAsync(Reports report)
        {
            await _context.Reports.AddAsync(report);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateReportAsync(Reports report)
        {
            _context.Reports.Update(report);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReportAsync(int id)
        {
            var report = await _context.Reports.FindAsync(id);
            if (report != null)
            {
                _context.Reports.Remove(report);
                await _context.SaveChangesAsync();
            }
        }
    }
}
