using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Application.Interfaces;
using Application;
using Application.Models.Requests;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]

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
    public ActionResult<List<MedicDto>> GetAll()
    {
        return _medicService.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<MedicDto> GetById([FromRoute]int id)
    {
        try
        {
            return Ok(_medicService.GetById(id));
        }
        catch (System.Exception)
        {
            
            throw;
        }

    }

    [HttpPost]
    public IActionResult Create(MedicCreateRequest medicCreateRequest)
    {
        _medicService.Create(medicCreateRequest);
        return Ok();

    }

    [HttpPut("{id}")]
    public IActionResult Update([FromRoute] int id, [FromBody] MedicUpdateRequest medicUpdateRequest )
    {
        try
        {
             _medicService.Update(id, medicUpdateRequest);
             return Ok();
            
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        try
        {
             _medicService.Delete(id);
             return Ok();
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }

    [HttpGet("[action]")]
    public ActionResult<List<MedicDto>> GetMedicsBySpecialty(int specialtyId)
    {
        var specialty = _specialtyService.GetById(specialtyId);

        if (specialty == null)
        {
            return NotFound($"No se encontr� ninguna especialidad con ID {specialtyId}");
        }

        var medics = _medicService.GetAll();

        // Filtrar los m�dicos que tienen la especialidad espec�fica
        var medicsInSpecialty = medics.Where(m => m.Specialties.Any(s => s.Id == specialty.Id)).ToList();

        return Ok(medicsInSpecialty);
    }

}