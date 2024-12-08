using Gold_Mining_Management_System.Models;
using Gold_Mining_Management_System.Data;
using Microsoft.EntityFrameworkCore;

namespace Gold_Mining_Management_System.Repositories
{
    public class EnvironmentalDataRepository : IEnvironmentalDataRepository
    {
        private readonly AppDbContext _context;

        public EnvironmentalDataRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EnvironmentalData>> GetAllEnvironmentalDataAsync()
        {
            return await _context.EnvironmentalData
                .Include(ed => ed.Officer).Include(p => p.Site)
                .ToListAsync();
        }

        public async Task<EnvironmentalData> GetEnvironmentalDataByIdAsync(int id)
        {
            return await _context.EnvironmentalData
                .Include(ed => ed.Officer).Include(p => p.Site)
                .FirstOrDefaultAsync(ed => ed.AssessmentId == id);
        }

        public async Task AddEnvironmentalDataAsync(EnvironmentalData data)
        {
            _context.EnvironmentalData.Add(data);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEnvironmentalDataAsync(EnvironmentalData data)
        {
            _context.EnvironmentalData.Update(data);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEnvironmentalDataAsync(int id)
        {
            var data = await _context.EnvironmentalData.FindAsync(id);
            if (data != null)
            {
                _context.EnvironmentalData.Remove(data);
                await _context.SaveChangesAsync();
            }
        }
    }
}
