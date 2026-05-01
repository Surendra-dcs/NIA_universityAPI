using Microsoft.AspNetCore.Mvc;
using NIAUNIVERSITYPANELAPI.Models;
using NIAUNIVERSITYPANELAPI.Service;

[Route("api/[controller]")]
[ApiController]
public class RemunerationController : ControllerBase
{
    private readonly RemunerationService _service;
    public RemunerationController(RemunerationService service)
    {
        _service = service;
    }

    [HttpGet("GetBill")]
    public async Task<IActionResult> GetBill()
    {
        var data = await _service.GetBillAsync();

        if (data == null || data.Count == 0)
            return NotFound("No data found");

        return Ok(data);
    }
}