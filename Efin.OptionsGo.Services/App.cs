using Efin.OptionsGo.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Efin.OptionsGo.Services
{
  public class App : AppBase
  {
    public App(AppDb db) : base(db)
    {
      Portfolios = new PortfolioService(this);
      Users = new UserService(this);
    }

    public UserService Users { get; }

    public PortfolioService Portfolios { get; }
  }
}
