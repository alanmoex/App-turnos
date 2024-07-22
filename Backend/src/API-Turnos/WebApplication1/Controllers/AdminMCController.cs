using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.Models.Requests;
using Application;
using Microsoft.AspNetCore.Authorization;
using Domain.Exceptions;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "SysAdmin")]
public class AdminMCController : ControllerBase
{
    private readonly IAdminMCService _adminMCService;
    public AdminMCController(IAdminMCService adminMCService)
    {
        _adminMCService = adminMCService;
    }

    [HttpGet]
    public ActionResult<List<AdminMCDto>> GetAll()
    {
        return Ok(_adminMCService.GetAll());
    }

    [HttpPost]
    public IActionResult Create(AdminMCCreateRequest adminMCCreateRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var adminMC = _adminMCService.Create(adminMCCreateRequest);
            return CreatedAtAction(nameof(GetById), new { id = adminMC.Id }, adminMC);
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An unexpected error occurred.");
        }
    }

    [HttpGet("{id}")]
    public ActionResult<AdminMCDto> GetById(int id)
    {
        try
        {
            return Ok(_adminMCService.GetById(id));
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An unexpected error occurred.");
        }
    }


    [HttpPut("{id}")]
    public IActionResult Update(int id, AdminMCUpdateRequest adminMCUpdateRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            _adminMCService.Update(id, adminMCUpdateRequest);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An unexpected error occurred.");
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            _adminMCService.Delete(id);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An unexpected error occurred.");
        }
    }
}