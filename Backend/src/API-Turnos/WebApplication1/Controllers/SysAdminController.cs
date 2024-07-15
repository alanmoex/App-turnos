using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Application.Interfaces;
using Domain.Entities;
using Application.Models.Requests;
using Application;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SysAdminController : ControllerBase
{
    private readonly ISysAdminService _sysAdminService;
    public SysAdminController(ISysAdminService sysadminService)
    {
        _sysAdminService = sysadminService;
    }

    [HttpGet]
    public ActionResult<List<SysAdminDto>> GetAll()
    {
        var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

        if (userRole != typeof(SysAdmin).Name)
            return Forbid();

        return _sysAdminService.GetAll();
    }

    [HttpPost]
    public IActionResult Create(SysAdminCreateRequest sysAdminCreateRequest)
    {
        var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

        if (userRole != typeof(SysAdmin).Name)
            return Forbid();

        return Ok(_sysAdminService.Create(sysAdminCreateRequest));
    }

    [HttpGet("{id}")]
    public ActionResult<SysAdminDto> GetById(int id)
    {
        try
        {
            return _sysAdminService.GetById(id);
        }
        catch (System.Exception)
        {
            return NotFound();
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, SysAdminUpdateRequest sysAdminUpdateRequest)
    {
        var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

        if (userRole != typeof(SysAdmin).Name)
            return Forbid();

        try
        {
            _sysAdminService.Update(id, sysAdminUpdateRequest);
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

        if (userRole != typeof(SysAdmin).Name)
            return Forbid();

        try
        {
            _sysAdminService.Delete(id);
            return Ok();
        }
        catch (System.Exception)
        {
            return NotFound();
        }
    }



}

