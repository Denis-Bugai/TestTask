using Microsoft.AspNetCore.Mvc;
using TestTaskDenysBuhai.Services;

namespace TestTaskDenysBuhai.Controllers
{
	[Route("api/Configure")]
	public class ConfigureController : ControllerBase
    {
		IConfigureService _configureService;
		public ConfigureController(IConfigureService configureService)
		{
			_configureService = configureService;
		}
		[HttpGet("GetSettings")]
        public IActionResult GetSettings()
        {
            return Ok(_configureService.GetSettings());
        }
		[HttpPost("SetSettings")]
		public IActionResult SetSettings(string documentType, int maxLenght)
		{
			return Ok(_configureService.SetSettings(documentType, maxLenght));
		}
	}
}