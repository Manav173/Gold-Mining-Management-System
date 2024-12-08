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
    public class MinePlansController : ControllerBase
    {
        private readonly IMinePlansService _service;
        private readonly IUserRepository _userRepository;
        private readonly ISiteRepository _siteRepository;

        public MinePlansController(IMinePlansService service, IUserRepository userRepository, ISiteRepository siteRepository)
        {
            _service = service;
            _userRepository = userRepository;
            _siteRepository = siteRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Mine Manager, Geologist, Engineer, Safety Officer, Field Worker")]
        public async Task<IActionResult> GetAllMinePlans()
        {
            var minePlans = await _service.GetAllMinePlansAsync();
            return Ok(minePlans);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Mine Manager, Geologist, Engineer, Safety Officer, Field Worker")]
        public async Task<IActionResult> GetMinePlanById(int id)
        {
            var minePlan = await _service.GetMinePlanByIdAsync(id);
            if (minePlan == null)
                return NotFound();
            return Ok(minePlan);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Mine Manager, Engineer")]
        public async Task<IActionResult> CreateMinePlan([FromBody] MinePlans minePlan)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var engineer = await _userRepository.GetUserById(minePlan.AssignedEngineer);
            if (engineer == null || engineer.Role != "Engineer")
            {
                ModelState.AddModelError("AssignedEngineer", "Invalid Engineer ID.");
                return BadRequest(ModelState);
            }
            minePlan.Engineer = engineer;
            var site = await _siteRepository.GetByIdAsync(minePlan.SiteId);
            if (site == null)
            {
                ModelState.AddModelError("SiteId", "Invalid Site ID.");
                return BadRequest(ModelState);
            }
            minePlan.Site = site;
            await _service.AddMinePlanAsync(minePlan);
            return CreatedAtAction(nameof(GetMinePlanById), new { id = minePlan.PlanId }, minePlan);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Mine Manager, Engineer")]
        public async Task<IActionResult> UpdateMinePlan(int id, [FromBody] MinePlans minePlan)
        {
            if (id != minePlan.PlanId || !ModelState.IsValid)
                return BadRequest();
            var engineer = await _userRepository.GetUserById(minePlan.AssignedEngineer);
            if (engineer == null || engineer.Role != "Engineer")
            {
                ModelState.AddModelError("AssignedEngineer", "Invalid Engineer ID.");
                return BadRequest(ModelState);
            }
            minePlan.Engineer = engineer;
            var site = await _siteRepository.GetByIdAsync(minePlan.SiteId);
            if (site == null)
            {
                ModelState.AddModelError("SiteId", "Invalid Site ID.");
                return BadRequest(ModelState);
            }
            minePlan.Site = site;
            await _service.UpdateMinePlanAsync(minePlan);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteMinePlan(int id)
        {
            await _service.DeleteMinePlanAsync(id);
            return NoContent();
        }

        [HttpGet("Counts")]
        [AllowAnonymous]
        public async Task<IActionResult> GetMinePlanCounts()
        {
            var counts = await _service.GetMinePlanCountsAsync();
            return Ok(counts);
        }
    }
}
