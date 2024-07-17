using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Application.Interfaces;
using Domain.Entities;
using Application.Models.Requests;
using Application;
using Microsoft.AspNetCore.Authorization;
<<<<<<< HEAD
=======
using Domain.Entities;
>>>>>>> development
using System.Security.Claims;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
<<<<<<< HEAD
=======

>>>>>>> development
public class AdminMCController : ControllerBase
{   
    private readonly IAdminMCService _adminMCService;
    public AdminMCController(IAdminMCService adminMCService)
    {
        _adminMCService = adminMCService;
    }

    [HttpGet]
    public ActionResult<List<AdminMCDto>> GetAll()
    {
        var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

        if (userRole != typeof(AdminMC).Name && userRole != typeof(SysAdmin).Name)
            return Forbid();

        return _adminMCService.GetAll();
    }

    [HttpPost]
    public IActionResult Create(AdminMCCreateRequest adminMCCreateRequest)
    {
        var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

<<<<<<< HEAD
        if (userRole != typeof(SysAdmin).Name)
            return Forbid();

        return Ok(_adminMCService.Create(adminMCCreateRequest));
=======
        if (userRole != typeof(AdminMC).Name && userRole != typeof(SysAdmin).Name)
            return Forbid();

        return Ok(_adminMCService.Create(adminMCCreateRequest)); 
        
>>>>>>> development
    }

    [HttpGet("{id}")]
    public ActionResult<AdminMCDto> GetById(int id)
    {
        return _adminMCService.GetById(id);
    }

    [HttpGet("[action]/{id}")]
    public async Task<ActionResult<AdminMCDto>> GetByIdAsync(int id)
    {
        return await _adminMCService.GetByIdAsync(id);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, AdminMCUpdateRequest adminMCUpdateRequest)
    {
        var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

        if (userRole != typeof(AdminMC).Name && userRole != typeof(SysAdmin).Name)
            return Forbid();

       
        try
        {
            _adminMCService.Update(id, adminMCUpdateRequest);
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
            _adminMCService.Delete(id);
            return Ok();
        }
        catch (System.Exception)
        {
            return NotFound();
        }
    }

    

}



