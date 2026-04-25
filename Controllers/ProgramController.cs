using Microsoft.AspNetCore.Mvc;
using NIAUNIVERSITYPANELAPI.Models;
using NIAUNIVERSITYPANELAPI.Service;

[Route("api/[controller]")]
[ApiController]
public class ProgramController : ControllerBase
{
    private readonly ProgramService _service;
    public ProgramController(ProgramService service)
    {
        _service = service;
    }
    [HttpGet("GetPrograms")]
    public IActionResult GetPrograms()
    {
        var data = _service.GetPrograms();
        return Ok(data);
    }    
   
    [HttpPost("Add")]
    public IActionResult AddProgram([FromBody] ProModel model)
    {
        _service.AddProgram(model);
        return Ok("Added");
    }

    [HttpPut("Update")]
    public IActionResult UpdateProgram([FromBody] ProModel model)
    {
        _service.UpdateProgram(model);
        return Ok("Updated");
    }
    [HttpDelete("Delete/{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            bool result = _service.DeleteProgram(id);
            if (result)
                return Ok(new { message = "Deleted successfully" });
            else
                return NotFound(new { message = "Record not found" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

}