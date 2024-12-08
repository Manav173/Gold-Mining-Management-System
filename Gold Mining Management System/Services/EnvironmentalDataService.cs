using Gold_Mining_Management_System.Models;
using Gold_Mining_Management_System.Repositories;

namespace Gold_Mining_Management_System.Services
{
    public class EnvironmentalDataService : IEnvironmentalDataService
    {
        private readonly IEnvironmentalDataRepository _repository;

        public EnvironmentalDataService(IEnvironmentalDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<EnvironmentalData>> GetAllEnvironmentalDataAsync()
        {
            return await _repository.GetAllEnvironmentalDataAsync();
        }

        public async Task<EnvironmentalData> GetEnvironmentalDataByIdAsync(int id)
        {
            return await _repository.GetEnvironmentalDataByIdAsync(id);
        }

        public async Task AddEnvironmentalDataAsync(EnvironmentalData data)
        {
            await _repository.AddEnvironmentalDataAsync(data);
        }

        public async Task UpdateEnvironmentalDataAsync(EnvironmentalData data)
        {
            await _repository.UpdateEnvironmentalDataAsync(data);
        }

        public async Task DeleteEnvironmentalDataAsync(int id)
        {
            await _repository.DeleteEnvironmentalDataAsync(id);
        }
    }
}
