using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Domain.Exceptions;
using Application;
using Domain.Entities;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MedicController : ControllerBase
{
    private readonly IMedicService _medicService;
    private readonly ISpecialtyService _specialtyService;

    public MedicController(IMedicService medicService, ISpecialtyService specialtyService)
    {
        _medicService = medicService;
        _specialtyService = specialtyService;
    }

    [HttpGet]
    [Authorize(Roles = "SysAdmin,AdminMC,Patient")]
    public ActionResult<List<MedicDto>> GetAll()
    {
        return Ok(_medicService.GetAll());
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "SysAdmin,AdminMC,Patient")]
    public ActionResult<MedicDto> GetById(int id)
    {
        try
        {
            return Ok(_medicService.GetById(id));
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
    [Authorize(Roles = "SysAdmin,AdminMC")]
    public IActionResult Create(MedicCreateRequest medicCreateRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var medic = _medicService.Create(medicCreateRequest);
            return CreatedAtAction(nameof(GetById), new { id = medic.Id }, medic);
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
    [Authorize(Roles = "SysAdmin,AdminMC")]
    public IActionResult Update(int id, MedicUpdateRequest medicUpdateRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            _medicService.Update(id, medicUpdateRequest);
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
    [Authorize(Roles = "SysAdmin,AdminMC")]
    public IActionResult Delete(int id)
    {
        try
        {
            _medicService.Delete(id);
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

    [HttpGet("[action]")]
    [Authorize(Roles = "SysAdmin,AdminMC,Patient")]
    public ActionResult<List<MedicDto>> GetMedicsBySpecialty(int specialtyId)
    {
        try
        {
            var specialty = _specialtyService.GetById(specialtyId);
            var medics = _medicService.GetAll();
            var medicsInSpecialty = medics.Where(m => m.Specialties.Any(s => s.Id == specialty.Id)).ToList();

            return Ok(medicsInSpecialty);
        }
        catch (NotFoundException ex)
        {
            return NotFound("Specialty not found");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An unexpected error occurred.");
        }
    }
}
