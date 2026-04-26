using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NIAUNIVERSITYPANELAPI.Models;
using NIAUNIVERSITYPANELAPI.Service;

namespace NIAUNIVERSITYPANELAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ProgramService _service;
        public CourseController(ProgramService service)
        {
            _service = service;
        }
        [HttpGet("GetCourses")]
        public IActionResult GetCourses()
        {
            return Ok(_service.GetCoursest());
        }

        [HttpGet("GetPrograms")]
        public IActionResult GetPrograms()
        {
            return Ok(_service.GetPrograms());
        }
        [HttpGet("GetExamDetails")]
        public IActionResult GetExamDetails()
        {
            return Ok(_service.GetExam());
        }
        

        [HttpPost("Add")]
        public IActionResult Add([FromBody] Course model)
        {
            _service.AddCourse(model);
            return Ok("add");
        }

        [HttpPut("Update")]
        public IActionResult Update([FromBody] Course model)
        {
            _service.UpdateCourse(model);
            return Ok("update");
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                bool result = _service.DeleteCourse(id);
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
}
