using Gold_Mining_Management_System.Models;
using Gold_Mining_Management_System.Repositories;
using Gold_Mining_Management_System.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gold_Mining_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionController : ControllerBase
    {
        private readonly IProductionService _service;
        private readonly IUserRepository _userRepository;
        private readonly ISiteService _siteRepository;

        public ProductionController(IProductionService service, IUserRepository userRepository, ISiteService siteRepository)
        {
            _service = service;
            _userRepository = userRepository;
            _siteRepository = siteRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Mine Manager, Geologist, Engineer, Safety Officer, Field Worker")]
        public async Task<IActionResult> GetAllProductions()
        {
            var productions = await _service.GetAllProductionsAsync();
            return Ok(productions);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Mine Manager, Geologist, Engineer, Safety Officer, Field Worker")]
        public async Task<IActionResult> GetProductionById(int id)
        {
            var production = await _service.GetProductionByIdAsync(id);
            if (production == null) return NotFound();
            return Ok(production);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Mine Manager, Geologist, Engineer, Safety Officer, Field Worker")]
        public async Task<IActionResult> AddProduction([FromBody] Production production)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var site = await _siteRepository.GetSiteByIdAsync(production.SiteId);
            if (site != null)
            {
                production.Site = site;
            }

            if (production.ShiftSupervisor.HasValue)
            {
                var supervisor = await _userRepository.GetUserById(production.ShiftSupervisor.Value);
                if (supervisor == null || supervisor.Role != "Mine Manager")
                {
                    ModelState.AddModelError("ShiftSupervisor", "Invalid Shift Supervisor ID.");
                    return BadRequest(ModelState);
                }
                production.Supervisor = supervisor; 
            }
            
            await _service.AddProductionAsync(production);
            return CreatedAtAction(nameof(GetProductionById), new { id = production.ProductionId }, production);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Mine Manager")]
        public async Task<IActionResult> UpdateProduction(int id, [FromBody] Production production)
        {
            if (id != production.ProductionId)
                return BadRequest("Production ID mismatch.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var site = await _siteRepository.GetSiteByIdAsync(production.SiteId);
            if (site == null)
            {
                ModelState.AddModelError("SiteId", "Invalid Site ID.");
                return BadRequest(ModelState);
            }
            if (production.ShiftSupervisor.HasValue)
            {
                var supervisor = await _userRepository.GetUserById(production.ShiftSupervisor.Value);
                if (supervisor == null || supervisor.Role != "Mine Manager")
                {
                    ModelState.AddModelError("ShiftSupervisor", "Invalid Shift Supervisor ID.");
                    return BadRequest(ModelState);
                }
                production.Supervisor = supervisor;
            }
            production.Site = site;
            await _service.UpdateProductionAsync(production);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Mine Manager")]
        public async Task<IActionResult> DeleteProduction(int id)
        {
            var existingProduction = await _service.GetProductionByIdAsync(id);
            if (existingProduction == null)
                return NotFound();

            await _service.DeleteProductionAsync(id);
            return NoContent();
        }

        [HttpGet("daily")]
        [Authorize(Roles = "Admin, Mine Manager, Geologist, Engineer, Safety Officer, Field Worker")]
        public async Task<IActionResult> GetDailyProduction()
        {
            var data = await _service.GetDailyProductionAsync();
            return Ok(data);
        }

        [HttpGet("weekly")]
        [Authorize(Roles = "Admin, Mine Manager, Geologist, Engineer, Safety Officer, Field Worker")]
        public async Task<IActionResult> GetWeeklyProduction()
        {
            var data = await _service.GetWeeklyProductionAsync();
            return Ok(data);
        }

        [HttpGet("monthly")]
        [Authorize(Roles = "Admin, Mine Manager, Geologist, Engineer, Safety Officer, Field Worker")]
        public async Task<IActionResult> GetMonthlyProduction()
        {
            var data = await _service.GetMonthlyProductionAsync();
            return Ok(data);
        }

        [HttpGet("counts")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductionCounts()
        {
            var counts = await _service.GetProductionCountsAsync();
            return Ok(counts);
        }
    }
}
