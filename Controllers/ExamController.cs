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

        [HttpGet("rolllist")]
        public IActionResult rolllist()
        {
            return Ok(_service.GetRolllist());
        }


        [HttpGet("resultlist")]
        public IActionResult resultlist()
        {
            return Ok(_service.Getresultlist());
        }

        [HttpGet]
        [Route("resultsheet")]
        public IActionResult GetResultList(int? examId = null, string? rollNo = null)
        {
            var data = _service.GetTabulationRegister(examId, rollNo);
            return Ok(data);
        }

        [HttpGet("revallist")]
        public IActionResult RevalList()
        {
            return Ok(_service.GetReEvaluationList());
        }
    }
}
