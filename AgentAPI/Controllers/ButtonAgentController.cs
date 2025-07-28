//using Microsoft.AspNetCore.Mvc;

//[ApiController]
//[Route("api/[controller]")]
//public class ButtonAgentController : ControllerBase
//{
//	private readonly ButtonStatusService _statusService;
//	public ButtonAgentController(ButtonStatusService statusService)
//	{
//		_statusService = statusService;
//	}

//	[HttpGet("status")]
//	public IActionResult GetButtonStatus()
//	{
		
//		bool pressed = _statusService.CurrentStatus == "BASILI";
//		return Ok(new { pressed, raw = _statusService.CurrentStatus });
//	}
//}
