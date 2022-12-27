using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Efin.OptionsGo.Models.Tests
{
  public class PortfolioTest
  {
    public class AddOrder
    {
      [Fact]
      public void AddOneOrder()
      {
        // a
        Portfolio p = new Portfolio();
        Assert.Empty(p.Orders);

        // a
        Order? o = p.AddOrder("LF @975 x 1");

        // a
        Assert.NotNull(o);
        Assert.Single(p.Orders);
        var x = p.Orders.First();
        Assert.Equal("S50X99", x.Symbol);
      }

      [Fact]
      public void InvalidOrder_NotAddedToPort()
      {
        Portfolio p = new Portfolio();
        Order? o = p.AddOrder("");
        Assert.Null(o);
        Assert.Empty(p.Orders);
      }
    }

    public class ProfitLoss
    {
      [Fact]
      public void LongFu()
      {
        Portfolio p = new();
        p.AddOrder("LF @975 x 1");
        p.Index = 977.0;

        double pl = p.ProfitLoss;

        Assert.Equal(2.0, pl);
      }

      [Fact]
      public void ShortFu()
      {
        Portfolio p = new();
        p.AddOrder("SF @975 x 1");
        p.Index = 977.0;

        double pl = p.ProfitLoss;

        Assert.Equal(-2.0, pl);
      }


      [Fact]
      public void ShortFuAndTakeProfit()
      {
        Portfolio p = new();
        p.AddOrder("SF @975 x 2");
        p.AddOrder("LF @950 x 2");

        p.Index = 950.0;

        double pl = p.ProfitLoss;

        Assert.Equal(50.0, pl);
      }

      [Fact(Skip = "homework")]
      public void LongCall()
      {
        Portfolio p = new();
        var o = p.AddOrder("LC 1000 @8.5 x 1");

        Assert.Equal(-8.5, o.CalculatePL(980));
        Assert.Equal(-8.5, o.CalculatePL(990));
        Assert.Equal(-8.5, o.CalculatePL(1000));
        Assert.Equal(-7.5, o.CalculatePL(1001));
        Assert.Equal(-6.5, o.CalculatePL(1002));
        Assert.Equal(0, o.CalculatePL(1008.5));
        Assert.Equal(10, o.CalculatePL(1018.5));
      }
    }
  }
}
