using Microsoft.AspNetCore.Mvc;
using NIAUNIVERSITYPANELAPI.Models;
using NIAUNIVERSITYPANELAPI.Service;

namespace NIAUNIVERSITYPANELAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MobileChangeRequestController : ControllerBase
    {
        private readonly MobileChangeRequestService _service;

        public MobileChangeRequestController(MobileChangeRequestService service)
        {
            _service = service;
        }

        // ── GET api/MobileChangeRequest/all?status=Pending ────────────────────
        [HttpGet("all")]
        public IActionResult GetAll([FromQuery] string? status = null)
        {
            var list = _service.GetAll(status);
            return Ok(list);
        }

        // ── POST api/MobileChangeRequest/{id}/approve ─────────────────────────
        [HttpPost("{id:int}/approve")]
        public IActionResult Approve(int id)
        {
            var (success, message) = _service.Approve(id);
            if (!success)
                return BadRequest(new { message });

            return Ok(new { message });
        }

        // ── POST api/MobileChangeRequest/{id}/reject ──────────────────────────
        [HttpPost("{id:int}/reject")]
        public IActionResult Reject(int id)
        {
            var (success, message) = _service.Reject(id);
            if (!success)
                return BadRequest(new { message });

            return Ok(new { message });
        }
    }
}