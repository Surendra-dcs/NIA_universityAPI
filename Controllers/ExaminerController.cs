using Microsoft.AspNetCore.Mvc;
using NIAUNIVERSITYPANELAPI.Models;
using NIAUNIVERSITYPANELAPI.Service;

[Route("api/[controller]")]
[ApiController]
public class ExaminerController : ControllerBase
{
    private readonly ExaminerService _service;
    public ExaminerController(ExaminerService service)
    {
        _service = service;
    }

    [HttpPost("Save")]
    public async Task<JsonResult> Save([FromBody] ExaminerModel model)
    {
        var id = await _service.Save(model);
        return new JsonResult(new { success = true, id });
    }

    [HttpGet("GetAll")]
    public async Task<JsonResult> GetAll()
    {
        return new JsonResult(await _service.GetAll());
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteExaminerAsync(id);
        return Ok(new { success = true, message = "Examiner deleted successfully" });
    }

    [HttpGet("GetExaminer/{id}")]
    public async Task<JsonResult> GetById(int id)
    {
        var data = await _service.GetById(id);
        return new JsonResult(data);
    }

    [HttpGet("GetColleges")]
    public async Task<IActionResult> GetColleges()
    {
        var data = await _service.GetColleges();
        return Ok(data);
    }

    [HttpGet("GetCourses")]
    public async Task<IActionResult> GetCourses()
    {
        var data = await _service.GetCourses();
        return Ok(data);
    }

    [HttpGet("GetSubjects")]
    public async Task<IActionResult> GetSubjects(int courseId)
    {
        var data = await _service.GetSubjects(courseId);
        return Ok(data);
    }

    [HttpGet("GetExams")]
    public async Task<IActionResult> GetExams(int courseId)
    {
        var data = await _service.GetExams(courseId);
        return Ok(data);
    }

    [HttpPost("AssignPaper")]
    public async Task<IActionResult> AssignPaper([FromBody] AssignPaperModel model)
    {
        if (model.ExaminerId <= 0)
            return BadRequest("Invalid Examiner");

        bool result = await _service.AssignPaper(model);

        if (result)
            return Ok(new { message = "Saved successfully" });

        return StatusCode(500, "Insert failed");
    }

    [HttpGet("GetAllAssignedExaminer")]
    public async Task<JsonResult> GetAllAssignedExaminer()
    {
        return new JsonResult(await _service.GetAllAssignedExaminer());
    }

}