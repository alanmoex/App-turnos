using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Application.Interfaces;
using Application;
using Application.Models.Requests;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]

public class MedicalCenterController : ControllerBase
{
    private readonly IMedicalCenterService _medicalCenterService;
    public MedicalCenterController(IMedicalCenterService medicalCenterService)
    {
        _medicalCenterService = medicalCenterService;
    }

    [HttpGet]
    public ActionResult<List<MedicalCenterDto>> GetAll()
    {
        return _medicalCenterService.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<MedicalCenterDto> GetById([FromRoute] int id)
    {
        try
        {
            return Ok(_medicalCenterService.GetById(id));
        }
        catch (System.Exception)
        {

            throw;
        }

    }

    [HttpPost]
    public IActionResult Create(MedicalCenterCreateRequest medicalCenterCreateRequest)
    {
        return Ok(_medicalCenterService.Create(medicalCenterCreateRequest));

    }

    [HttpPut("{id}")]
    public IActionResult Update([FromRoute] int id, [FromBody] MedicalCenterUpdateRequest medicalCenterUpdateRequest)
    {
        try
        {
            _medicalCenterService.Update(id, medicalCenterUpdateRequest);
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
            _medicalCenterService.Delete(id);
            return Ok();
        }
        catch (System.Exception)
        {

            throw;
        }
    }
}