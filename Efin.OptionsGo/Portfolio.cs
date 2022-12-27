using System.ComponentModel.DataAnnotations;

namespace Efin.OptionsGo.Models
{
  public class Portfolio
  {
    public Portfolio()
    {
      Orders = new HashSet<Order>();
    }

    public Guid Id { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = "Portfolio";

    public bool IsActive { get; set; } = true;

    public virtual ICollection<Order> Orders { get; set; }

    public double Index { get; set; }
    public double ProfitLoss
      => Orders.Sum(x => x.CalculatePL(Index));

    public DateTimeOffset? CreatedDate { get; set; }

    public Order? AddOrder(string text)
    {
      var o = Order.FromText(text);
      if (o != null)
      {
        Orders.Add(o);
      }
      return o;
    }
  }
}