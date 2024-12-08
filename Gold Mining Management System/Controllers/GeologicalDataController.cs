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
    public class GeologicalDataController : ControllerBase
    {
        private readonly IGeologicalDataService _service;
        private readonly IUserRepository _userRepository;
        private readonly IReportService _reportService;
        private readonly ISiteService _siteService;

        public GeologicalDataController(
            IGeologicalDataService service,
            IUserRepository userRepository,
            IReportService reportService,
            ISiteService siteService)
        {
            _service = service;
            _userRepository = userRepository;
            _reportService = reportService;
            _siteService = siteService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Mine Manager, Geologist, Engineer, Safety Officer, Field Worker")]
        public async Task<IActionResult> GetAllGeologicalData()
        {
            var data = await _service.GetAllGeologicalDataAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Mine Manager, Geologist, Engineer, Safety Officer, Field Worker")]
        public async Task<IActionResult> GetGeologicalDataById(int id)
        {
            var data = await _service.GetGeologicalDataByIdAsync(id);
            if (data == null)
                return NotFound();
            return Ok(data);
        }

        [HttpGet("Site{id}")]
        [Authorize(Roles = "Admin, Manager, Geologist")]
        public async Task<IActionResult> GetGeologicalDataBySiteId(int id)
        {
            var data = await _service.GetGeologicalDataBySiteIdAsync(id);
            if (data == null)
                return NotFound();
            return Ok(data);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Geologist")]
        public async Task<IActionResult> CreateGeologicalData([FromBody] GeologicalData geologicalData)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var report = await _reportService.GetReportByIdAsync(geologicalData.ReportId);
            if (report != null)
            {
                geologicalData.SurveyReport = report;
            }

            var geologist = await _userRepository.GetUserById(geologicalData.GeologistId);
            if (geologist != null)
            {
                if (geologist.Role != "Geologist")
                {
                    ModelState.AddModelError("GeologistId", "Invalid Geologist ID.");
                    return BadRequest(ModelState);
                }
                geologicalData.Geologist = geologist;
            }

            var site = await _siteService.GetSiteByIdAsync(geologicalData.SiteId);
            if (site !=  null)
            {
                geologicalData.Site = site;
            }

            await _service.AddGeologicalDataAsync(geologicalData);
            return CreatedAtAction(nameof(GetGeologicalDataById), new { id = geologicalData.DataId }, geologicalData);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Geologist")]
        public async Task<IActionResult> UpdateGeologicalData(int id, [FromBody] GeologicalData geologicalData)
        {
            if (id != geologicalData.DataId || !ModelState.IsValid)
                return BadRequest();

            var report = await _reportService.GetReportByIdAsync(geologicalData.ReportId);
            if (report != null)
            {
                geologicalData.SurveyReport = report;
            }

            var geologist = await _userRepository.GetUserById(geologicalData.GeologistId);
            if (geologist != null)
            {
                if (geologist.Role != "Geologist")
                {
                    ModelState.AddModelError("GeologistId", "Invalid Geologist ID.");
                    return BadRequest(ModelState);
                }
                geologicalData.Geologist = geologist;
            }

            var site = await _siteService.GetSiteByIdAsync(geologicalData.SiteId);
            if (site != null)
            {
                geologicalData.Site = site;
            }

            await _service.UpdateGeologicalDataAsync(geologicalData);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteGeologicalData(int id)
        {
            await _service.DeleteGeologicalDataAsync(id);
            return NoContent();
        }

        [HttpGet("Count")]
        [AllowAnonymous]
        public async Task<IActionResult> GetGeologicalDataCounts()
        {
            var counts = await _service.GetGeologicalDataCountsAsync();
            return Ok(counts);
        }
    }
}
