using Gold_Mining_Management_System.Models;
using Gold_Mining_Management_System.Data;
using Microsoft.EntityFrameworkCore;
using Gold_Mining_Management_System.Services;
using Gold_Mining_Management_System.DTO;

namespace Gold_Mining_Management_System.Repositories
{
    public class SafetyIncidentRepository : ISafetyIncidentRepository
    {
        private readonly AppDbContext _context;

        public SafetyIncidentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SafetyIncident>> GetAllAsync()
        {
            return await _context.SafetyIncidents
                .Include(i => i.Reporter) 
                .ToListAsync();
        }

        public async Task<SafetyIncident> GetByIdAsync(int incidentId)
        {
            return await _context.SafetyIncidents
                .Include(i => i.Reporter) 
                .FirstOrDefaultAsync(i => i.IncidentId == incidentId);
        }

        public async Task AddAsync(SafetyIncident incident)
        {
            await _context.SafetyIncidents.AddAsync(incident);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SafetyIncident incident)
        {
            _context.SafetyIncidents.Update(incident);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int incidentId)
        {
            var incident = await _context.SafetyIncidents.FindAsync(incidentId);
            if (incident != null)
            {
                _context.SafetyIncidents.Remove(incident);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<SafetyIncidentCounts> GetSafetyIncidentCountsAsync()
        {
            var totalIncidents = await _context.Set<SafetyIncident>().CountAsync();
            var resolvedIncidents = await _context.Set<SafetyIncident>().CountAsync(i => i.ResolutionStatus == "Resolved");
            var pendingIncidents = await _context.Set<SafetyIncident>().CountAsync(i => i.ResolutionStatus == "Pending");

            var lowSeverity = await _context.Set<SafetyIncident>().CountAsync(i => i.Severity == "Low");
            var mediumSeverity = await _context.Set<SafetyIncident>().CountAsync(i => i.Severity == "Medium");
            var highSeverity = await _context.Set<SafetyIncident>().CountAsync(i => i.Severity == "High");

            return new SafetyIncidentCounts
            {
                TotalSafetyIncidents = totalIncidents,
                ResolvedIncidents = resolvedIncidents,
                PendingIncidents = pendingIncidents,
                LowSeverity = lowSeverity,
                MediumSeverity = mediumSeverity,
                HighSeverity = highSeverity
            };
        }
    }
}
