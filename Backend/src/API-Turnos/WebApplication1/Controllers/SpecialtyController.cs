using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Application;
using Domain.Entities;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SpecialtyController : ControllerBase
{
    private readonly ISpecialtyService _specialtyService;
    public SpecialtyController(ISpecialtyService specialtyService){
        _specialtyService = specialtyService;
    }

    [HttpPost]
    public IActionResult Create([FromBody] SpecialtyCreateRequest specialtyCreateRequest){
        return Ok(_specialtyService.Create(specialtyCreateRequest));
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id){
        _specialtyService.Delete(id);
        return Ok();
    }

    [HttpGet]
    public ActionResult<List<SpecialtyDto>> GetAll(){
        return _specialtyService.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<SpecialtyDto> GetById(int id){
        return _specialtyService.GetById(id);
    }

    [HttpPut]
    public IActionResult Update(int id, [FromBody] SpecialtyUpdateRequest specialtyUpdateRequest){        
        try
        {
            _specialtyService.Update(id, specialtyUpdateRequest);
            return Ok();
        }
        catch (System.Exception)
        {
            return NotFound();
        }
    }
}
