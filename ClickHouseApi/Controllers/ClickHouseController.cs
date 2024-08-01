using Microsoft.AspNetCore.Mvc;
using ClickHouseApi.Helpers;

namespace ClickHouseApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClickHouseController : ControllerBase
    {
        private readonly ClickHouseHelper _clickHouseHelper;

        public ClickHouseController(ClickHouseHelper clickHouseHelper)
        {
            _clickHouseHelper = clickHouseHelper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
