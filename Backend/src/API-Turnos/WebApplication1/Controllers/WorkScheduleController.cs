using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Application.Interfaces;
using Domain.Entities;
using Application.Models.Requests;
using Application;
using Microsoft.AspNetCore.Authorization;
using Domain.Entities;
using System.Security.Claims;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class WorkScheduleController : ControllerBase
{   
    private readonly IWorkScheduleService _workScheduleService;
    public WorkScheduleController(IWorkScheduleService workScheduleService)
    {
        _workScheduleService = workScheduleService;
    }

    [HttpGet]
    public ActionResult<List<WorkScheduleDto>> GetAll()
    {
        var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

        if (userRole != typeof(AdminMC).Name && userRole != typeof(SysAdmin).Name)
            return Forbid();
        return _workScheduleService.GetAll();
    }

    [HttpPost]
    public IActionResult Create(WorkScheduleCreateRequest workScheduleCreateRequest)
    {
        var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

        if (userRole != typeof(AdminMC).Name && userRole != typeof(SysAdmin).Name)
            return Forbid();
        return Ok(_workScheduleService.Create(workScheduleCreateRequest));
    }

    [HttpGet("{id}")]
    public ActionResult<WorkScheduleDto> GetById(int id)
    {
        try
        {
            return _workScheduleService.GetById(id);
        }
        catch (System.Exception)
        {
            return NotFound();
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, WorkScheduleUpdateRequest workScheduleUpdateRequest)
    {
        var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

        if (userRole != typeof(AdminMC).Name && userRole != typeof(SysAdmin).Name)
            return Forbid();
            
        try
        {
            _workScheduleService.Update(id, workScheduleUpdateRequest);
            return Ok();
        }
        catch (System.Exception)
        {
            return BadRequest();
        }
    }


    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

        if (userRole != typeof(AdminMC).Name && userRole != typeof(SysAdmin).Name)
            return Forbid();

        try
        {
            _workScheduleService.Delete(id);
            return Ok();
        }
        catch (System.Exception)
        {
            return NotFound();
        }
    }

    

}

