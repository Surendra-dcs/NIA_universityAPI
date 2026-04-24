using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NIAUNIVERSITYPANELAPI.Models;
using NIAUNIVERSITYPANELAPI.Service;
using System.Data;

namespace NIAUNIVERSITYPANELAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _service;

        public SubjectController(ISubjectService service)
        {
            _service = service;
        }

        [HttpPost("save")]
        public IActionResult SaveSubject([FromBody] SubjectModel model)
        {
            var result = _service.SaveSubject(model);
            return Ok(result);
        }

        [HttpGet("list")]
        public IActionResult GetSubjects()
        {
            return Ok(_service.GetSubjects());
        }

        [HttpGet("courses/{programId}")]
        public IActionResult GetCourses(int programId)
        {
            return Ok(_service.GetCoursesByProgram(programId));
        }
        [HttpGet("get/{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_service.GetSubjectById(id));
        }
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(_service.DeleteSubject(id));
        }
    }
}
