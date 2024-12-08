using Gold_Mining_Management_System.Models;

namespace Gold_Mining_Management_System.Services
{
    public interface IEnvironmentalDataService
    {
        public Task<IEnumerable<EnvironmentalData>> GetAllEnvironmentalDataAsync();
        public Task<EnvironmentalData> GetEnvironmentalDataByIdAsync(int id);
        public Task AddEnvironmentalDataAsync(EnvironmentalData data);
        public Task UpdateEnvironmentalDataAsync(EnvironmentalData data);
        public Task DeleteEnvironmentalDataAsync(int id);
    }
}
