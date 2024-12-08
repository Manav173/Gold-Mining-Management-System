using Gold_Mining_Management_System.DTO;
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
    public class SafetyIncidentController : ControllerBase
    {
        private readonly ISafetyIncidentService _service;
        private readonly IUserRepository _userRepository;

        public SafetyIncidentController(ISafetyIncidentService service, IUserRepository userRepository)
        {
            _service = service;
            _userRepository = userRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Mine Manager, Geologist, Engineer, Safety Officer, Field Worker")]
        public async Task<IActionResult> GetAllIncidents()
        {
            var incidents = await _service.GetAllIncidentsAsync();
            return Ok(incidents);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Mine Manager, Geologist, Engineer, Safety Officer, Field Worker")]
        public async Task<IActionResult> GetIncidentById(int id)
        {
            var incident = await _service.GetIncidentByIdAsync(id);
            if (incident == null)
                return NotFound();
            return Ok(incident);
        }

        [HttpPost]
        [Authorize(Roles = "Safety Officer, Admin")]
        public async Task<IActionResult> CreateIncident([FromBody] SafetyIncident incident)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reporter = await _userRepository.GetUserById(incident.ReportedBy);
            if (reporter == null || reporter.Role != "Safety Officer")
            {
                ModelState.AddModelError("ManagerId", "Invalid Manager ID.");
                return BadRequest(ModelState);
            }

            incident.Reporter = reporter;
            await _service.AddIncidentAsync(incident);

            return CreatedAtAction(nameof(GetIncidentById), new { id = incident.IncidentId }, incident);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Safety Officer, Admin")]
        public async Task<IActionResult> UpdateIncident(int id, [FromBody] SafetyIncident incident)
        {
            if (id != incident.IncidentId || !ModelState.IsValid)
                return BadRequest();
            var reporter = await _userRepository.GetUserById(incident.ReportedBy);
            if (reporter == null || reporter.Role != "Safety Officer")
            {
                ModelState.AddModelError("ManagerId", "Invalid Manager ID.");
                return BadRequest(ModelState);
            }
            incident.Reporter = reporter;
            await _service.UpdateIncidentAsync(incident);
            return CreatedAtAction(nameof(GetIncidentById), new { id = incident.IncidentId }, incident);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteIncident(int id)
        {
            await _service.DeleteIncidentAsync(id);
            return NoContent();
        }

        [HttpGet("counts")]
        [AllowAnonymous]
        public async Task<ActionResult<SafetyIncidentCounts>> GetSafetyIncidentCounts()
        {
            try
            {
                var counts = await _service.GetSafetyIncidentCountsAsync();
                return Ok(counts);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request: " + ex.Message);
            }
        }
    }
}
