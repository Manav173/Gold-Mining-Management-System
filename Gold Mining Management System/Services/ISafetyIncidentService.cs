using Gold_Mining_Management_System.DTO;
using Gold_Mining_Management_System.Models;

namespace Gold_Mining_Management_System.Services
{
    public interface ISafetyIncidentService
    {
        public Task<IEnumerable<SafetyIncident>> GetAllIncidentsAsync();
        public Task<SafetyIncident> GetIncidentByIdAsync(int incidentId);
        public Task AddIncidentAsync(SafetyIncident incident);
        public Task UpdateIncidentAsync(SafetyIncident incident);
        public Task DeleteIncidentAsync(int incidentId);
        public Task<SafetyIncidentCounts> GetSafetyIncidentCountsAsync();
    }
}
