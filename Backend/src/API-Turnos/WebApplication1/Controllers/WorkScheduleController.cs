using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Domain.Exceptions;
using Application;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "AdminMC, SysAdmin")]
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
        return Ok(_workScheduleService.GetAll());
    }

    [HttpPost]
    public IActionResult Create([FromBody] WorkScheduleCreateRequest workScheduleCreateRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var workSchedule = _workScheduleService.Create(workScheduleCreateRequest);
            return CreatedAtAction(nameof(GetById), new { id = workSchedule.Id }, workSchedule);
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

    [HttpGet("{id}")]
    public ActionResult<WorkScheduleDto> GetById(int id)
    {
        try
        {
            return Ok(_workScheduleService.GetById(id));
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An unexpected error occurred.");
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] WorkScheduleUpdateRequest workScheduleUpdateRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            _workScheduleService.Update(id, workScheduleUpdateRequest);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An unexpected error occurred.");
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            _workScheduleService.Delete(id);
            return NoContent();
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
}


