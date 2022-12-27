using Efin.OptionsGo.Services;
using Efin.OptionsGo.Services.Data;
using Microsoft.EntityFrameworkCore;

namespace Efin.OptionsGO.API
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      // Add services to the container.

      builder.Services.AddControllers();
      // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
      builder.Services.AddEndpointsApiExplorer();
      builder.Services.AddSwaggerGen();

      builder.Services.AddDbContext<AppDb>(options =>
      {
        //options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(AppDb)));
        options.UseSqlServer(builder.Configuration["ConnectionStrings:AppDb"]);
      });

      builder.Services.AddScoped<App>();

      var app = builder.Build();

      // Configure the HTTP request pipeline.
      if (app.Environment.IsDevelopment())
      {
        app.UseSwagger();
        app.UseSwaggerUI();
      }

      app.UseHttpsRedirection();

      app.UseAuthorization();


      app.MapControllers();

      app.Run();
    }
  }
}