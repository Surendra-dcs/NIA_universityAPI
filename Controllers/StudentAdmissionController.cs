using Microsoft.AspNetCore.Mvc;
using NIAUNIVERSITYPANELAPI.Models;
using NIAUNIVERSITYPANELAPI.Service;

[Route("api/[controller]")]
[ApiController]
public class StudentAdmissionController : ControllerBase
{
    private readonly StudentAdmissionService _service;
    public StudentAdmissionController(StudentAdmissionService service)
    {
        _service = service;
    }
    [HttpGet("GetStudentAdmissionList")]
    public IActionResult GetStudentAdmissionList()
    {
        var data = _service.GetStudentAdmissionList();
        return Ok(data);
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
    public IActionResult UpdateStudent([FromBody] StudentAdmissionModel model)
    {
        var result = _service.UpdateStudent(model);

        if (result)
            return Ok("Updated successfully");

        return BadRequest("Update failed");
    }

}