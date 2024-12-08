using Gold_Mining_Management_System.Models;
using Gold_Mining_Management_System.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gold_Mining_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentsController : ControllerBase
    {
        private readonly IEquipmentsService _service;
        private readonly ISiteService _siteService;

        public EquipmentsController(IEquipmentsService service, ISiteService siteService)
        {
            _service = service;
            _siteService = siteService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Mine Manager, Geologist, Engineer, Safety Officer, Field Worker")]
        public async Task<IActionResult> GetAllEquipments()
        {
            var equipments = await _service.GetAllEquipmentsAsync();
            return Ok(equipments);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Mine Manager, Geologist, Engineer, Safety Officer, Field Worker")]
        public async Task<IActionResult> GetEquipmentById(int id)
        {
            var equipment = await _service.GetEquipmentByIdAsync(id);
            if (equipment == null)
                return NotFound();
            return Ok(equipment);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Mine Manager, Engineer")]
        public async Task<IActionResult> CreateEquipment([FromBody] Equipments equipment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (equipment.Condition == "Needs Repair" && equipment.AssignedSite.HasValue)
            {
                ModelState.AddModelError("AssignedSite", "Equipment with 'Needs Repair' condition cannot be assigned to a site.");
                return BadRequest(ModelState);
            }

            if (equipment.AssignedSite.HasValue)
            {
                var site = await _siteService.GetSiteByIdAsync(equipment.AssignedSite.Value);
                if (site == null)
                {
                    ModelState.AddModelError("AssignedSite", "Invalid Site Id.");
                    return BadRequest(ModelState);
                }
                equipment.Site = site;
            }

            await _service.AddEquipmentAsync(equipment);
            return CreatedAtAction(nameof(GetEquipmentById), new { id = equipment.EquipmentId }, equipment);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Mine Manager, Engineer")]
        public async Task<IActionResult> UpdateEquipment(int id, [FromBody] Equipments equipment)
        {
            if (id != equipment.EquipmentId || !ModelState.IsValid)
                return BadRequest();

            if (equipment.Condition == "Needs Repair" && equipment.AssignedSite.HasValue)
            {
                ModelState.AddModelError("AssignedSite", "Equipment with 'Needs Repair' condition cannot be assigned to a site.");
                return BadRequest(ModelState);
            }

            if (equipment.AssignedSite.HasValue)
            {
                var site = await _siteService.GetSiteByIdAsync(equipment.AssignedSite.Value);
                if (site == null)
                {
                    ModelState.AddModelError("AssignedSite", "Invalid Site Id.");
                    return BadRequest(ModelState);
                }
                equipment.Site = site;
            }
            if (!equipment.AssignedSite.HasValue)
                equipment.Site = null;
            await _service.UpdateEquipmentAsync(equipment);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEquipment(int id)
        {
            await _service.DeleteEquipmentAsync(id);
            return NoContent();
        }

        [HttpGet("counts")]
        [AllowAnonymous]
        public async Task<IActionResult> GetEquipmentCountsAsync()
        {
            var equipmentCounts = await _service.GetEquipmentCountsAsync();
            return Ok(equipmentCounts);
        }
    }
}
