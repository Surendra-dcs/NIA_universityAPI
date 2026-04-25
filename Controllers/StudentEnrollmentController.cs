using Microsoft.AspNetCore.Mvc;
using NIAUNIVERSITYPANELAPI.Models;
using NIAUNIVERSITYPANELAPI.Service;

[Route("api/[controller]")]
[ApiController]
public class StudentEnrollmentController : ControllerBase
{
    private readonly StudentEnrollmentService _service;
    public StudentEnrollmentController(StudentEnrollmentService service)
    {
        _service = service;
    }
    [HttpGet("GetStudentEnrollmentList")]
    public IActionResult GetStudentEnrollmentList()
    {
        var data = _service.GetStudentEnrollmentList();
        return Ok(data);
    }

    [HttpPost]
    [Route("VerifyStudent")]
    public IActionResult VerifyStudent([FromBody] StudentEnrollmentModel model)
    {
        if (model.Id <= 0)
            return BadRequest("Invalid Id");

        bool result = _service.VerifyStudent(model);

        if (result)
            return Ok();

        return StatusCode(500, "Verification failed");
    }

}