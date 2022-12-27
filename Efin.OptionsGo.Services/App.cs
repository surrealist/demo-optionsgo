using Efin.OptionsGo.Models;
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

    public PortfolioService Portfolios { get; }

    public UserService Users { get; }
    public User? CurrentUser { get; private set; } = null;
    public bool IsAuthenticated => CurrentUser != null;

    public void SetCurrentUser(Guid id, string username, string role)
    {
      var user = Users.Find(id);
      if (user == null)
      {
        user = new User
        {
          Id = id,
          Name = username,
          CreatedDate = Now(),
          Role = role,
          Note = null
        };
        Users.Add(user);
        SaveChanges();
      }

      CurrentUser = user;
    }
  }
}
