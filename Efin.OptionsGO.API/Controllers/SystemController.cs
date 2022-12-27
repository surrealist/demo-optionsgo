using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Efin.OptionsGO.API.Controllers
{
  [Route("api/v1/[controller]")]
  [ApiController]
  public class SystemController : ControllerBase
  {

    [HttpGet("info")]
    public ActionResult Info()
    {
      return Ok(new
      {
        Version = "0.1.0",
        Environment = "Development",
      });
    }
  }
}
