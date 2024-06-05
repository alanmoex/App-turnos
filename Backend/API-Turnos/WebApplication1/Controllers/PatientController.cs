using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Application.Interfaces;
using Domain.Entities;
using Application.Models.Requests;
using Application;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientController : ControllerBase
{   
    private readonly IPatientService _patientService;
    public PatientController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpGet("{id}")]
    public ActionResult<PatientDto> GetById(int id)
    {
        try
        {
            return _patientService.GetById(id);
        }
        catch (System.Exception)
        {
            return NotFound();
        }
    }

    [HttpGet]
    public ActionResult<List<PatientDto>> GetAll()
    {
        return _patientService.GetAll();
    }

    [HttpPost]
    public IActionResult Create(PatientCreateRequest patientCreateRequest)
    {
        return Ok(_patientService.Create(patientCreateRequest));
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            _patientService.Delete(id);
            return Ok();
        }
        catch (System.Exception)
        {
            return NotFound();
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, PatientUpdateRequest patientUpdateRequest)
    {
        try
        {
            _patientService.Update(id, patientUpdateRequest);
            return Ok();
        }
        catch (System.Exception)
        {
            return BadRequest();
        }
    }

}
