using Microsoft.AspNetCore.Mvc;
using Application.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Domain.Exceptions;
using Application;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SpecialtyController : ControllerBase
{
    private readonly ISpecialtyService _specialtyService;

    public SpecialtyController(ISpecialtyService specialtyService)
    {
        _specialtyService = specialtyService;
    }

    [HttpGet]
    [Authorize(Roles = "Patient, AdminMC, SysAdmin")]
    public ActionResult<List<SpecialtyDto>> GetAll()
    {
        return Ok(_specialtyService.GetAll());
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Patient, AdminMC, SysAdmin")]
    public ActionResult<SpecialtyDto> GetById(int id)
    {
        try
        {
            return Ok(_specialtyService.GetById(id));
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

    [HttpPost]
    [Authorize(Roles = "AdminMC, SysAdmin")]
    public IActionResult Create([FromBody] SpecialtyCreateRequest specialtyCreateRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var specialty = _specialtyService.Create(specialtyCreateRequest);
            return CreatedAtAction(nameof(GetById), new { id = specialty.Id }, specialty);
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
    [Authorize(Roles = "AdminMC, SysAdmin")]
    public IActionResult Delete(int id)
    {
        try
        {
            _specialtyService.Delete(id);
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

    [HttpPut("{id}")]
    [Authorize(Roles = "AdminMC, SysAdmin")]
    public IActionResult Update(int id, [FromBody] SpecialtyUpdateRequest specialtyUpdateRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            _specialtyService.Update(id, specialtyUpdateRequest);
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
