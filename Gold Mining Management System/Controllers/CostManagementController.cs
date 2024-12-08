using Gold_Mining_Management_System.Models;
using Gold_Mining_Management_System.Repositories;
using Gold_Mining_Management_System.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gold_Mining_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CostManagementController : ControllerBase
    {
        private readonly ICostManagementService _service;
        private readonly IUserRepository _userRepository;
        private readonly ISiteRepository _siteRepository;

        public CostManagementController(ICostManagementService service, IUserRepository userRepository, ISiteRepository siteRepository)
        {
            _service = service;
            _userRepository = userRepository;
            _siteRepository = siteRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Mine Manager, Geologist, Engineer, Safety Officer, Field Worker")]
        public async Task<IActionResult> GetAllCosts()
        {
            var costs = await _service.GetAllCostsAsync();
            return Ok(costs);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Mine Manager, Geologist, Engineer, Safety Officer, Field Worker")]
        public async Task<IActionResult> GetCostById(int id)
        {
            var cost = await _service.GetCostByIdAsync(id);
            if (cost == null)
                return NotFound();
            return Ok(cost);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Mine Manager")]
        public async Task<IActionResult> CreateCost([FromBody] CostManagement cost)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var site = await _siteRepository.GetByIdAsync(cost.SiteId);
            if (site == null)
            {
                ModelState.AddModelError("SiteId", "Invalid Site ID.");
                return BadRequest(ModelState);
            }
            if (cost.ResponsiblePerson.HasValue)
            {
                var manager = await _userRepository.GetUserById(cost.ResponsiblePerson.Value);
                if (manager == null || manager.Role != "Mine Manager")
                {
                    ModelState.AddModelError("ResponsiblePerson", "Invalid Responsible Person ID.");
                    return BadRequest(ModelState);
                }
                cost.Manager = manager; 
            }
            cost.Site = site;
            await _service.AddCostAsync(cost);
            return CreatedAtAction(nameof(GetCostById), new { id = cost.CostId }, cost);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Mine Manager")]
        public async Task<IActionResult> UpdateCost(int id, [FromBody] CostManagement cost)
        {
            if (id != cost.CostId || !ModelState.IsValid)
                return BadRequest();

            var site = await _siteRepository.GetByIdAsync(cost.SiteId);
            if (site == null)
            {
                ModelState.AddModelError("SiteId", "Invalid Site ID.");
                return BadRequest(ModelState);
            }
            if (cost.ResponsiblePerson.HasValue)
            {
                var manager = await _userRepository.GetUserById(cost.ResponsiblePerson.Value);
                if (manager == null || manager.Role != "Mine Manager")
                {
                    ModelState.AddModelError("ResponsiblePerson", "Invalid Responsible Person ID.");
                    return BadRequest(ModelState);
                }
                cost.Manager = manager;
            }
            cost.Site = site;
            await _service.UpdateCostAsync(cost);
            return CreatedAtAction(nameof(GetCostById), new { id = cost.CostId }, cost);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCost(int id)
        {
            await _service.DeleteCostAsync(id);
            return Ok($"Cost with ID {id} has been deleted.");
        }

        [HttpGet("counts")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCostManagementCounts()
        {
            var counts = await _service.GetCostManagementCountsAsync();
            return Ok(counts);
        }
    }
}
