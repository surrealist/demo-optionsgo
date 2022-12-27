using Efin.OptionsGo.Services.Data;

namespace Efin.OptionsGo.Services
{
  public abstract class AppBase
  {
    internal readonly AppDb db;

    public AppBase(AppDb db) => this.db = db;

    public int SaveChanges() => db.SaveChanges();
    public Task<int> SaveChangesAsync() => db.SaveChangesAsync();

    public Func<DateTimeOffset> Now { get; private set; } = () => DateTimeOffset.Now;
    public void SetNow(DateTimeOffset now) => Now = () => now;
    public void ResetNow() => Now = () => DateTimeOffset.Now;
    public DateTimeOffset Today() => Now().Date;
  }
}
