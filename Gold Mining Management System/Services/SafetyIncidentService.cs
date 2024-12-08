using Gold_Mining_Management_System.DTO;
using Gold_Mining_Management_System.Models;
using Gold_Mining_Management_System.Repositories;

namespace Gold_Mining_Management_System.Services
{
    public class SafetyIncidentService : ISafetyIncidentService
    {
        private readonly ISafetyIncidentRepository _repository;

        public SafetyIncidentService(ISafetyIncidentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SafetyIncident>> GetAllIncidentsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<SafetyIncident> GetIncidentByIdAsync(int incidentId)
        {
            return await _repository.GetByIdAsync(incidentId);
        }

        public async Task AddIncidentAsync(SafetyIncident incident)
        {
            await _repository.AddAsync(incident);
        }

        public async Task UpdateIncidentAsync(SafetyIncident incident)
        {
            await _repository.UpdateAsync(incident);
        }

        public async Task DeleteIncidentAsync(int incidentId)
        {
            await _repository.DeleteAsync(incidentId);
        }

        public async Task<SafetyIncidentCounts> GetSafetyIncidentCountsAsync()
        {
            return await _repository.GetSafetyIncidentCountsAsync();
        }
    }
}
