using Gold_Mining_Management_System.Models;
using Gold_Mining_Management_System.Data;
using Microsoft.EntityFrameworkCore;
using Gold_Mining_Management_System.DTO;

namespace Gold_Mining_Management_System.Repositories
{
    public class EquipmentsRepository : IEquipmentsRepository
    {
        private readonly AppDbContext _context;

        public EquipmentsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Equipments>> GetAllEquipmentsAsync()
        {
            return await _context.Equipments
                .Include(e => e.Site) // Including related Site entity
                .ToListAsync();
        }

        public async Task<Equipments> GetEquipmentByIdAsync(int id)
        {
            return await _context.Equipments
                .Include(e => e.Site)
                .FirstOrDefaultAsync(e => e.EquipmentId == id);
        }

        public async Task AddEquipmentAsync(Equipments equipment)
        {
            _context.Equipments.Add(equipment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEquipmentAsync(Equipments equipment)
        {
            _context.Equipments.Update(equipment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEquipmentAsync(int id)
        {
            var equipment = await _context.Equipments.FindAsync(id);
            if (equipment != null)
            {
                _context.Equipments.Remove(equipment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<EquipmentCounts> GetEquipmentCountsAsync()
        {
            var totalEquipments = await _context.Equipments.CountAsync();
            var goodEquipments = await _context.Equipments.CountAsync(e => e.Condition == "Good");
            var needsRepair = await _context.Equipments.CountAsync(e => e.Condition == "Needs Repair");
            var availableEquipments = await _context.Equipments.CountAsync(e => e.AssignedSite == null);

            return new EquipmentCounts
            {
                TotalEquipments = totalEquipments,
                GoodEquipments = goodEquipments,
                NeedsRepair = needsRepair,
                AvailableEquipments = availableEquipments
            };
        }
    }
}
