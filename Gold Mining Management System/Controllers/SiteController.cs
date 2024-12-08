using Gold_Mining_Management_System.Models;
using Gold_Mining_Management_System.Repositories;
using Gold_Mining_Management_System.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

[Route("api/[controller]")]
[ApiController]
public class SiteController : ControllerBase
{
    private readonly ISiteService _siteService;
    private readonly IUserRepository _userRepository;

    public SiteController(ISiteService siteService, IUserRepository userRepository)
    {
        _siteService = siteService;
        _userRepository = userRepository;
    }

    [HttpGet]
    [Authorize(Roles = "Admin, Mine Manager, Geologist, Engineer, Safety Officer, Field Worker")]
    public async Task<IActionResult> GetAllSitesAsync()
    {
        var sites = await _siteService.GetAllSitesAsync();
        return Ok(sites);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin, Mine Manager, Geologist, Engineer, Safety Officer, Field Worker")]
    public async Task<IActionResult> GetSiteById(int id)
    {
        var site = await _siteService.GetSiteByIdAsync(id);
        if (site == null)
        {
            return NotFound();
        }
        return Ok(site);
    }

    [HttpPost]
    [Authorize(Roles = "Mine Manager, Admin")]
    public async Task<IActionResult> CreateSiteAsync([FromBody] Sites site)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        if (site.ManagerId.HasValue)
        {
            var manager = await _userRepository.GetUserById(site.ManagerId);
            if (manager.Role != "Mine Manager")
            {
                ModelState.AddModelError("ManagerId", "Invalid Manager ID as User Linked to that Id is not a Manager.");
                return BadRequest(ModelState);
            }
            site.Manager = manager;
        }
        else
        {
            site.Manager = null;
        }
        await _siteService.AddSiteAsync(site);

        var s = await _siteService.GetSiteByIdAsync(site.SiteId);
        if (s == null)
        {
            return NotFound();
        }
        return Ok(s);
    }


    [HttpPut("{id}")]
    [Authorize(Roles = "Mine Manager, Geologist, Engineer, Safety Officer, Admin")]
    public async Task<IActionResult> UpdateSiteAsync(int id, [FromBody] Sites site)
    {
        if (id != site.SiteId || !ModelState.IsValid)
            return BadRequest();
        if (site.ManagerId.HasValue && site.ManagerId > 0)
        {
            var manager = await _userRepository.GetUserById(site.ManagerId);
            if (manager.Role != "Mine Manager")
            {
                ModelState.AddModelError("ManagerId", "Invalid Manager ID as User Linked to that Id is not a Manager.");
                return BadRequest(ModelState);
            }
            site.Manager = manager;
        }
        else
        {
            site.Manager = null;
        }
        await _siteService.UpdateSiteAsync(site);
        var s = await _siteService.GetSiteByIdAsync(site.SiteId);
        return Ok(s);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteSiteAsync(int id)
    {
        await _siteService.DeleteSiteAsync(id);
        return Ok($"Site with ID {id} has been deleted.");
    }


    [HttpGet("counts")]
    [AllowAnonymous]
    public async Task<IActionResult> GetSiteCountsAsync()
    {
        var siteCounts = await _siteService.GetSiteCountsAsync();
        return Ok(siteCounts);
    }
}
