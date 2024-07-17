using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Application.Interfaces;
using Application;
using Application.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Domain.Entities;
using System.Security.Claims;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]

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
        var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

        if (userRole != typeof(AdminMC).Name && userRole != typeof(SysAdmin).Name && userRole != typeof(Patient).Name )
            return Forbid();

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
         var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

        if (userRole != typeof(AdminMC).Name && userRole != typeof(SysAdmin).Name && userRole != typeof(Patient).Name )
            return Forbid();

        return Ok(_medicalCenterService.Create(medicalCenterCreateRequest));

    }

    [HttpPut("{id}")]
    public IActionResult Update([FromRoute] int id, [FromBody] MedicalCenterUpdateRequest medicalCenterUpdateRequest)
    {
         var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

        if (userRole != typeof(AdminMC).Name && userRole != typeof(SysAdmin).Name && userRole != typeof(Patient).Name )
            return Forbid();

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
         var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

        if (userRole != typeof(AdminMC).Name && userRole != typeof(SysAdmin).Name && userRole != typeof(Patient).Name )
            return Forbid();

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