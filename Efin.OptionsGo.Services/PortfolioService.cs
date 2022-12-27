using Efin.OptionsGo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Efin.OptionsGo.Services
{
  public class PortfolioService : ServiceBase<Portfolio>
  {
    public PortfolioService(App app) : base(app)
    {
      //
    }

    public Portfolio Create(string name)
    {
      if (!app.IsAuthenticated)
      {
        throw new Exception("");
      }

      var p = new Portfolio();
      p.Name = name;
      p.IsActive = true;
      p.CreatedDate = app.Now();

      app.Portfolios.Add(p);
      app.SaveChanges();

      return p;
    }
  }
}
