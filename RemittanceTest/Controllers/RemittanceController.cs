using Microsoft.AspNetCore.Mvc;
using RemittanceTest.Services;

namespace RemittanceTest.Controllers
{  
    [ApiController]
    [Route("api/[controller]")]
    public class RemittanceController : ControllerBase
    {
        private readonly IRemittanceService _remittanceService;

        public RemittanceController(IRemittanceService remittanceService)
        {
            _remittanceService = remittanceService;
            
        }

        [HttpPost("{id}/cancel")]
        public IActionResult Cancel(int id)
        {
            var (isSuccess, message) = _remittanceService.CancelRemittance(id);

            if (message == "找不到指定的匯款資料。")
                return NotFound(new { message });

            if (!isSuccess)
                return BadRequest(new { message });

            return Ok(new { message });
        }
    }
}