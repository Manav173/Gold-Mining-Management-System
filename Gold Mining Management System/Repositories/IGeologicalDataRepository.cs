using Gold_Mining_Management_System.DTO;
using Gold_Mining_Management_System.Models;

namespace Gold_Mining_Management_System.Repositories
{
    public interface IGeologicalDataRepository
    {
        public Task<IEnumerable<GeologicalData>> GetAllGeologicalDataAsync();
        public Task<GeologicalData> GetGeologicalDataByIdAsync(int id);
        public Task<GeologicalData> GetGeologicalDataBySiteIdAsync(int id);
        public Task AddGeologicalDataAsync(GeologicalData geologicalData);
        public Task UpdateGeologicalDataAsync(GeologicalData geologicalData);
        public Task DeleteGeologicalDataAsync(int id);
        public Task<GeologicalDataCounts> FetchGeologicalDataCountsAsync();
    }
}
