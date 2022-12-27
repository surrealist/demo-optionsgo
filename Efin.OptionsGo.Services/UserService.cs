using Efin.OptionsGo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Efin.OptionsGo.Services
{
  public class UserService : ServiceBase<User>
  {
    public UserService(App app) : base(app)
    {
    }
  }
}
