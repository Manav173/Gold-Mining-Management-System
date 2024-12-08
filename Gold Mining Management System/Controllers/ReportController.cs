using Gold_Mining_Management_System.Models;
using Gold_Mining_Management_System.Services;
using Gold_Mining_Management_System.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gold_Mining_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _service;
        private readonly IUserRepository _userRepository;

        public ReportController(IReportService service, IUserRepository userRepository)
        {
            _service = service;
            _userRepository = userRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Mine Manager, Geologist, Engineer, Safety Officer, Field Worker")]
        public async Task<IActionResult> GetAllReports()
        {
            var reports = await _service.GetAllReportsAsync();
            return Ok(reports);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Mine Manager, Geologist, Engineer, Safety Officer, Field Worker")]
        public async Task<IActionResult> GetReportById(int id)
        {
            var report = await _service.GetReportByIdAsync(id);
            if (report == null)
                return NotFound();
            return Ok(report);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Mine Manager, Geologist, Engineer, Safety Officer, Field Worker")]
        public async Task<IActionResult> CreateReport([FromBody] Reports report)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if(report.CreatedBy.HasValue)
            {
                var creator = await _userRepository.GetUserById(report.CreatedBy);
                if (creator == null)
                {
                    ModelState.AddModelError("CreatorBy", "Invalid Creator ID.");
                    return BadRequest(ModelState);
                }
                report.Creator = creator;
            }
            report.Creator = null;
            await _service.AddReportAsync(report);
            return CreatedAtAction(nameof(GetReportById), new { id = report.ReportId }, report);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateReport(int id, [FromBody] Reports report)
        {
            if (id != report.ReportId || !ModelState.IsValid)
                return BadRequest();
            var creator = await _userRepository.GetUserById(report.CreatedBy);
            //if (creator == null)
            //{
            //    ModelState.AddModelError("CreatorBy", "Invalid Creator ID.");
            //    return BadRequest(ModelState);
            //}
            report.Creator = creator;
            await _service.UpdateReportAsync(report);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteReport(int id)
        {
            await _service.DeleteReportAsync(id);
            return NoContent();
        }
    }
}
