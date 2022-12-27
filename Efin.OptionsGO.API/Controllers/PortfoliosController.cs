using Efin.OptionsGo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Efin.OptionsGO.API.Controllers
{
  [Route("api/v1/[controller]")]
  [ApiController]
  public class PortfoliosController : ControllerBase
  {
    private readonly App app;

    public PortfoliosController(App app)
    {
      this.app = app;
    }

    [HttpGet]
    public int CountPorts()
    {
      return app.Portfolios.All().Count();
    } 
  }
}
