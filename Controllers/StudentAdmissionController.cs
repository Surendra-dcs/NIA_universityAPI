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

}