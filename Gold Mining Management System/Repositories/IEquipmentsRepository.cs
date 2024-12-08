using Gold_Mining_Management_System.DTO;
using Gold_Mining_Management_System.Models;

namespace Gold_Mining_Management_System.Repositories
{
    public interface IEquipmentsRepository
    {
        public Task<IEnumerable<Equipments>> GetAllEquipmentsAsync();
        public Task<Equipments> GetEquipmentByIdAsync(int id);
        public Task AddEquipmentAsync(Equipments equipment);
        public Task UpdateEquipmentAsync(Equipments equipment);
        public Task DeleteEquipmentAsync(int id);
        public Task<EquipmentCounts> GetEquipmentCountsAsync();
    }
}
