using Gold_Mining_Management_System.DTO;
using Gold_Mining_Management_System.Models;
using Gold_Mining_Management_System.Repositories;

namespace Gold_Mining_Management_System.Services
{
    public class EquipmentsService : IEquipmentsService
    {
        private readonly IEquipmentsRepository _repository;

        public EquipmentsService(IEquipmentsRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Equipments>> GetAllEquipmentsAsync()
        {
            return await _repository.GetAllEquipmentsAsync();
        }

        public async Task<Equipments> GetEquipmentByIdAsync(int id)
        {
            return await _repository.GetEquipmentByIdAsync(id);
        }

        public async Task AddEquipmentAsync(Equipments equipment)
        {
            await _repository.AddEquipmentAsync(equipment);
        }

        public async Task UpdateEquipmentAsync(Equipments equipment)
        {
            await _repository.UpdateEquipmentAsync(equipment);
        }

        public async Task DeleteEquipmentAsync(int id)
        {
            await _repository.DeleteEquipmentAsync(id);
        }

        public async Task<EquipmentCounts> GetEquipmentCountsAsync()
        {
            return await _repository.GetEquipmentCountsAsync();
        }
    }
}
