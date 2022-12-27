using Xunit.Abstractions;

namespace Efin.OptionsGo.Models.Tests
{
  public class OrderTest
  {
    public class FromText
    {
      private readonly ITestOutputHelper output;
      public FromText(ITestOutputHelper output) => this.output = output;

      [Fact]
      public void BasicLongFu()
      {
        // Arrange
        var text = "LF @950 x 1";

        // Act
        Order? o = Order.FromText(text);

        // Assert
        Assert.NotNull(o);
        output.WriteLine($"{o.Side} {o.Symbol} at {o.Price} for {o.Contracts} cons");
        Assert.Equal(expected: 950.0, actual: o.Price);
        Assert.Equal("S50X99", o.Symbol);
        Assert.Equal(OrderSide.Long, o.Side);
        Assert.Equal(1, o.Contracts);
      }

      [Fact]
      public void BasicShortFu()
      {
        // Arrange
        var text = "SF @950.5 x 1";

        // Act
        Order? o = Order.FromText(text);

        // Assert
        Assert.NotNull(o);
        Assert.Equal(expected: 950.5, actual: o.Price);
        Assert.Equal("S50X99", o.Symbol);
        Assert.Equal(OrderSide.Short, o.Side);
        Assert.Equal(1, o.Contracts);
      }

      [Fact]
      public void MultipleSpaces()
      {
        // Arrange
        var text = "SF    @950.5   x   1";

        // Act
        Order? o = Order.FromText(text);

        // Assert
        Assert.NotNull(o);
        Assert.Equal(expected: 950.5, actual: o.Price);
        Assert.Equal("S50X99", o.Symbol);
        Assert.Equal(OrderSide.Short, o.Side);
        Assert.Equal(1, o.Contracts);
      }

      [Theory]
      [InlineData("SF @950.45 x 1", 950.5)]
      [InlineData("SF @950.55 x 1", 950.6)]
      [InlineData("SF @950.65 x 1", 950.7)]
      public void ExtraPrecisions_RoundToOneDigit(string text, double price)
      {
        Order? o = Order.FromText(text);

        Assert.NotNull(o);
        Assert.Equal(expected: price, actual: o!.Price);
      }

      public static IEnumerable<object[]> GetInvalidOrders()
      {
        using var reader = new StreamReader("InvalidOrders.txt");

        string? s;
        while ((s = reader.ReadLine()) != null)
        {
          s = s.Trim().Replace("\"", "");
          s = s.Replace("\\t", "\t");
          yield return new object[] { s };
        }
      }

      [Theory]
      [MemberData(nameof(GetInvalidOrders))]
      public void InvalidText_ReturnsNull(string text)
      {
        Order? o = Order.FromText(text);
        Assert.Null(o);
      }

      // 

      [Fact]
      public void EmptyText_ReturnsNull()
      {
        var text = "";
        Order? o = Order.FromText(text);
        Assert.Null(o);
      }

      [Fact]
      public void InvalidSyntax_ReturnsNull()
      {
        var text = "XX @500 x 1";
        Order? o = Order.FromText(text);
        Assert.Null(o);
      }


      [Fact]
      public void BasicLongOptions()
      {
        // Arrange
        var text = "LC 1000 @2.5 x 1";

        // Act
        Order? o = Order.FromText(text);

        // Assert
        Assert.NotNull(o);
        Assert.Equal(expected: 2.5, actual: o.Price);
        Assert.Equal("S50X99C1000", o.Symbol);
        Assert.Equal(OrderSide.Long, o.Side);
        Assert.Equal(1000, o.StrikePrice);
        Assert.Equal(1, o.Contracts);
      }
    }
  }
}