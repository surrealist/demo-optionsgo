using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Efin.OptionsGo.Models
{
  public class Order
  {
    private const string BaseSymbol = "S50X99";

    public Order() { }
    public Order(string symbol, OrderSide side, double price, int contracts)
    {
      Symbol = symbol;
      Side = side;
      Price = price;
      Contracts = contracts;
      if (symbol.Length > BaseSymbol.Length + 1)
      {
        int strikePrice;
        if (int.TryParse(symbol.Substring(BaseSymbol.Length + 1), out strikePrice))
        {
          StrikePrice = strikePrice;
        }
      }
    }

    [Key]
    public int Id { get; set; }

    [StringLength(30)]
    public string Symbol { get; private set;  } = null!;
    public OrderSide Side { get; private set; }
    public double Price { get; private set; }
    public int Contracts { get; private set; }
    public int StrikePrice { get; private set; }

    public static Order? FromText(string text)
    {
      if (text == "") return null;

      string pattern = @"(?<side>[LS])(?<symbol>[FCP])\s*(?<strikePrice>\d*)\s*@(?<price>\d+(\.\d+)?)\s*x\s*(?<contracts>\d+)";

      Match match = Regex.Match(text, pattern);
      if (!match.Success) return null;

      var side = match.Groups["side"].Value;

      var symbol = match.Groups["symbol"].Value;
      var price = Math.Round(double.Parse(match.Groups["price"].Value),
                        1, MidpointRounding.AwayFromZero);
      var contracts = int.Parse(match.Groups["contracts"].Value);
      var orderSide = side == "L" ? OrderSide.Long : OrderSide.Short;
      var strikePrice = match.Groups["strikePrice"].Value ?? "0";

      var orderSymbol = "";
      if (symbol == "F")
      {
        orderSymbol = BaseSymbol;
      }
      else
      {
        orderSymbol = $"{BaseSymbol}{symbol}{strikePrice}";
      }

      var o = new Order(orderSymbol, orderSide, price, contracts);
      return o;
    }

    public double CalculatePL(double index)
    {
      if (Side == OrderSide.Long)
        return (index - Price) * Contracts;
      else if (Side == OrderSide.Short)
        return (Price - index) * Contracts;

      return 0;
    }
  }
}

// LF @950 x 1 => Symbol=S50X99, StrikePrice=0
// SF @950 x 1

// Long call, Short call, Long put, Short put
// LC 1000 @2.3 x 2 => Symbol=S50X99C1000, StrikePrice=1000, Price=2.3, Cons=2
// SC 1000 @2.5 x 1
// LP 950 @12.6 x 1 => Symbol=S50X99P950, StrikePrice=950
// SP 950 @15 x 1
