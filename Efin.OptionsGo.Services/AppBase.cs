using Efin.OptionsGo.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Efin.OptionsGo.Services
{
  public abstract class AppBase
  {
    private readonly AppDb db;

    public AppBase(AppDb db)
    {
      this.db = db;
    }
  }
}
