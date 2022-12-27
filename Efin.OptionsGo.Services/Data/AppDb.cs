using Efin.OptionsGo.Models;
using Microsoft.EntityFrameworkCore;

namespace Efin.OptionsGo.Services.Data
{
  public class AppDb : DbContext
  {
    public AppDb(DbContextOptions<AppDb> options): base(options)
    {
      //
    }

    public DbSet<Portfolio> Portfolios { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;

    public DbSet<User> Users { get; set; } = null!;
  }
}
