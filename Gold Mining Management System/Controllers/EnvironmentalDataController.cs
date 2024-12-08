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
    public class EnvironmentalDataController : ControllerBase
    {
        private readonly IEnvironmentalDataService _service;
        private readonly IUserRepository _userRepository;
        private readonly ISiteRepository _siteRepository;

        public EnvironmentalDataController(IEnvironmentalDataService service, IUserRepository userRepository, ISiteRepository siteRepository)
        {
            _service = service;
            _userRepository = userRepository;
            _siteRepository = siteRepository;
        }

        // GET: api/EnvironmentalData
        [HttpGet]
        [Authorize(Roles = "Admin, Mine Manager, Geologist, Engineer, Safety Officer, Field Worker")]
        public async Task<IActionResult> GetAllEnvironmentalData()
        {
            var data = await _service.GetAllEnvironmentalDataAsync();
            return Ok(data);
        }

        // GET: api/EnvironmentalData/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Mine Manager, Geologist, Engineer, Safety Officer, Field Worker")]
        public async Task<IActionResult> GetEnvironmentalDataById(int id)
        {
            var data = await _service.GetEnvironmentalDataByIdAsync(id);
            if (data == null)
                return NotFound();
            return Ok(data);
        }

        // POST: api/EnvironmentalData
        [HttpPost]
        [Authorize(Roles = "Admin, Geologist, Safety Officer")]
        public async Task<IActionResult> CreateEnvironmentalData([FromBody] EnvironmentalData data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (data.ConductedBy.HasValue)
            {
                var geologist = await _userRepository.GetUserById(data.ConductedBy.Value);
                if (geologist == null || geologist.Role != "Geologist")
                {
                    ModelState.AddModelError("ConductedBy", "Invalid Geologist ID.");
                    return BadRequest(ModelState);
                }
                data.Officer = geologist;
            }
            else
            {
                data.Officer = null; 
            }
            var site = await _siteRepository.GetByIdAsync(data.SiteId);
            if (site == null)
            {
                ModelState.AddModelError("SiteId", "Invalid Site ID.");
                return BadRequest(ModelState);
            }
            data.Site = site;
            await _service.AddEnvironmentalDataAsync(data);
            return CreatedAtAction(nameof(GetEnvironmentalDataById), new { id = data.AssessmentId }, data);
        }

        // PUT: api/EnvironmentalData/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Geologist, Safety Officer")]
        public async Task<IActionResult> UpdateEnvironmentalData(int id, [FromBody] EnvironmentalData data)
        {
            if (id != data.AssessmentId || !ModelState.IsValid)
                return BadRequest();
            if (data.ConductedBy.HasValue)
            {
                var geologist = await _userRepository.GetUserById(data.ConductedBy.Value);
                if (geologist == null || geologist.Role != "Geologist")
                {
                    ModelState.AddModelError("ConductedBy", "Invalid Geologist ID.");
                    return BadRequest(ModelState);
                }
                data.Officer = geologist;
            }
            else
            {
                data.Officer = null;
            }
            var site = await _siteRepository.GetByIdAsync(data.SiteId);
            if (site == null)
            {
                ModelState.AddModelError("SiteId", "Invalid Site ID.");
                return BadRequest(ModelState);
            }
            data.Site = site;
            await _service.UpdateEnvironmentalDataAsync(data);
            return NoContent();
        }

        // DELETE: api/EnvironmentalData/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEnvironmentalData(int id)
        {
            await _service.DeleteEnvironmentalDataAsync(id);
            return NoContent();
        }
    }
}
