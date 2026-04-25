using Microsoft.AspNetCore.Mvc;
using NIAUNIVERSITYPANELAPI.Models;
using NIAUNIVERSITYPANELAPI.Service;

[Route("api/[controller]")]
[ApiController]
public class StudentCorrectionController : ControllerBase
{
    private readonly StudentCorrectionService _service;
    public StudentCorrectionController(StudentCorrectionService service)
    {
        _service = service;
    }

    [HttpGet("GetStudentByMobile")]
    public IActionResult GetStudentByMobile(string mobile)
    {
        var data = _service.GetStudentByMobile(mobile);

        if (data == null)
            return NotFound("Student not found");

        return Ok(data);
    }

    [HttpPost("UpdateStudent")]
    public IActionResult UpdateStudent([FromBody] StudentCorrectionModel model)
    {
        var result = _service.UpdateStudent(model);

        if (result)
            return Ok("Updated successfully");

        return BadRequest("Update failed");
    }

    [HttpPost]
    [Route("UpdateMobile")]
    public IActionResult UpdateMobile([FromBody] UpdateMobileModel model)
    {
        if (string.IsNullOrEmpty(model.Mobile) || model.Mobile.Length != 10)
        {
            return BadRequest("Invalid mobile number");
        }

        bool result = _service.UpdateMobile(model);

        if (result)
            return Ok();

        return StatusCode(500, "Update failed");
    }

}