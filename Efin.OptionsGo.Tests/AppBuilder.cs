using Efin.OptionsGo.Models;
using Efin.OptionsGo.Services.Data;
using Efin.OptionsGo.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Efin.OptionsGo.Tests
{
  internal class AppBuilder
  {
    private readonly App _app;

    public AppBuilder()
    {
      var options = new DbContextOptionsBuilder<AppDb>()
        .UseInMemoryDatabase<AppDb>($"db-{Guid.NewGuid()}")
        .Options;
      var db = new AppDb(options);
      _app = new App(db);
    }

    public App Build() => _app;

    //public static Student Student_Alice = new Student
    //{
    //  Id = new Guid("7DB81C13-21F1-49C5-8521-7A57E3507CD9"),
    //  Name = "Alice",
    //  ImageUrl = "",
    //};
    //public static Teacher Teacher_Bob = new Teacher
    //{
    //  Id = new Guid("9A407EF9-B818-4F67-B12A-ABA224C192EC"),
    //  Name = "Bob",
    //  ImageUrl = "",
    //};
    //public static Course Course_Trumpet = new Course
    //{
    //  Id = "X-01",
    //  Name = "Trumpet",
    //  Hours = 4,
    //  Price = 4900
    //};

    public AppBuilder WithBasicScenario()
    {
      //var s1 = AppBuilder.Student_Alice;
      //var t1 = AppBuilder.Teacher_Bob;
      //var c1 = AppBuilder.Course_Trumpet;

      //_app.Students.Add(s1);
      //_app.Teachers.Add(t1);
      //_app.Courses.Add(c1);
      //_app.SaveChanges();
      return this;
    }

    public AppBuilder SetNow(DateTimeOffset now)
    {
      _app.SetNow(now);
      return this;
    }
    public AppBuilder LoginByAlice()
      => LoginBy(AppBuilder.User_Alice);

    public AppBuilder LoginBy(User u)
    {
      _app.SetCurrentUser(
        u.Id, u.Name, u.Role);

      return this;
    }

    public static User User_Alice
      = new User
      {
        Id = new Guid("1A5D3510-5FF7-4481-88E1-D39C0CB9B5B9"),
        Name = "Alic",
        Role = "Student"
      };

  }
}
