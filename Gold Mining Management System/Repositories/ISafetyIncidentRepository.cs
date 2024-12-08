using Gold_Mining_Management_System.DTO;
using Gold_Mining_Management_System.Models;

namespace Gold_Mining_Management_System.Repositories
{
    public interface ISafetyIncidentRepository
    {
        public Task<IEnumerable<SafetyIncident>> GetAllAsync();
        public Task<SafetyIncident> GetByIdAsync(int incidentId);
        public Task AddAsync(SafetyIncident incident);
        public Task UpdateAsync(SafetyIncident incident);
        public Task DeleteAsync(int incidentId);
        public Task<SafetyIncidentCounts> GetSafetyIncidentCountsAsync();
    }
}
