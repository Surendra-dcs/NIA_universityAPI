using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NIAUNIVERSITYPANELAPI.Models;
using NIAUNIVERSITYPANELAPI.Service;

namespace NIAUNIVERSITYPANELAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly IExamService _service;
        public ExamController(IExamService service)
        {
            _service = service;
        }
        [HttpPost("save")]
        public IActionResult Save(ExamModel model)
        {
            return Ok(_service.SaveExam(model));
        }
        [HttpGet("list")]
        public IActionResult List()
        {
            return Ok(_service.GetExams());
        }
        [HttpGet("get/{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_service.GetExamById(id));
        }
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(_service.DeleteExam(id));
        }
    }
}
