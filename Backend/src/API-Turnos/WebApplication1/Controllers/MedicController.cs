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
    public MedicController(IMedicService medicService)
    {
        _medicService = medicService;
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
            _medicService.GetById(id);
            return Ok();
        }
        catch (System.Exception)
        {
            
            throw;
        }

    }

    [HttpPost]
    public IActionResult Create(MedicCreateRequest medicCreateRequest)
    {
        return Ok(_medicService.Create(medicCreateRequest));

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

    [HttpGet("specialties/{medicId}")]
    public ActionResult<List<SpecialtyDto>> GetSpecialties(int medicId)
    {
        return _medicService.GetSpecialtiesByMedic(medicId).ToList();
    }
}