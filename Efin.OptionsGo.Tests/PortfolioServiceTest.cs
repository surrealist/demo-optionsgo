using Efin.OptionsGo.Models;
using Efin.OptionsGo.Services;
using Efin.OptionsGo.Services.Data;
using Microsoft.EntityFrameworkCore;

namespace Efin.OptionsGo.Tests
{
  public class PortfolioServiceTest
  {
    public class Create
    {
      [Fact]
      public void Authenticated_CanCreate()
      {
        var now = DateTimeOffset.Now;
        var app = new AppBuilder()
          .LoginByAlice()
          .SetNow(now)
          .Build();

        Assert.True(app.IsAuthenticated);

        Portfolio p = app.Portfolios.Create("MyPort");
        Assert.NotNull(p);
        Assert.Equal("MyPort", p.Name);
        Assert.Equal(now, p.CreatedDate);
      }

      [Fact]
      public void NoAuthen_CannotCreate()
      {
        var app = new AppBuilder().Build();

        Assert.False(app.IsAuthenticated);

        Assert.Throws<Exception>(() =>
        {
          var p = app.Portfolios.Create("Port-1");
        });

      }
    }
  }
}
