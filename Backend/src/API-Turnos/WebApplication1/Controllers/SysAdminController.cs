using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Domain.Exceptions;
using Application;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "SysAdmin")]
public class SysAdminController : ControllerBase
{
    private readonly ISysAdminService _sysAdminService;

    public SysAdminController(ISysAdminService sysAdminService)
    {
        _sysAdminService = sysAdminService;
    }

    [HttpGet]
    public ActionResult<List<SysAdminDto>> GetAll()
    {
        return Ok(_sysAdminService.GetAll());
    }

    [HttpPost]
    public IActionResult Create([FromBody] SysAdminCreateRequest sysAdminCreateRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var sysAdmin = _sysAdminService.Create(sysAdminCreateRequest);
            return CreatedAtAction(nameof(GetById), new { id = sysAdmin.Id }, sysAdmin);
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
    public ActionResult<SysAdminDto> GetById(int id)
    {
        try
        {
            return Ok(_sysAdminService.GetById(id));
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

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] SysAdminUpdateRequest sysAdminUpdateRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            _sysAdminService.Update(id, sysAdminUpdateRequest);
            return NoContent();
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

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            _sysAdminService.Delete(id);
            return NoContent();
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
}
