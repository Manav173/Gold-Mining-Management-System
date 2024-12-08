using Gold_Mining_Management_System.DTO;
using Gold_Mining_Management_System.Models;
using Gold_Mining_Management_System.Repositories;

namespace Gold_Mining_Management_System.Services
{
    public class GeologicalDataService : IGeologicalDataService
    {
        private readonly IGeologicalDataRepository _repository;

        public GeologicalDataService(IGeologicalDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<GeologicalData>> GetAllGeologicalDataAsync()
        {
            return await _repository.GetAllGeologicalDataAsync();
        }

        public async Task<GeologicalData> GetGeologicalDataByIdAsync(int id)
        {
            return await _repository.GetGeologicalDataByIdAsync(id);
        }

        public async Task<GeologicalData> GetGeologicalDataBySiteIdAsync(int id)
        {
            return await _repository.GetGeologicalDataBySiteIdAsync(id);
        }

        public async Task AddGeologicalDataAsync(GeologicalData geologicalData)
        {
            await _repository.AddGeologicalDataAsync(geologicalData);
        }

        public async Task UpdateGeologicalDataAsync(GeologicalData geologicalData)
        {
            await _repository.UpdateGeologicalDataAsync(geologicalData);
        }

        public async Task DeleteGeologicalDataAsync(int id)
        {
            await _repository.DeleteGeologicalDataAsync(id);
        }

        public async Task<GeologicalDataCounts> GetGeologicalDataCountsAsync()
        {
            return await _repository.FetchGeologicalDataCountsAsync();
        }
    }
}
