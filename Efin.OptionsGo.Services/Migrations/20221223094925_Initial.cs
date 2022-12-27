using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Efin.OptionsGo.Services.Migrations
{
  public partial class Initial : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "Portfolios",
          columns: table => new
          {
            Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            Index = table.Column<double>(type: "float", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Portfolios", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "Orders",
          columns: table => new
          {
            Id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            PortfolioId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Orders", x => x.Id);
            table.ForeignKey(
                      name: "FK_Orders_Portfolios_PortfolioId",
                      column: x => x.PortfolioId,
                      principalTable: "Portfolios",
                      principalColumn: "Id");
          });

      migrationBuilder.CreateIndex(
          name: "IX_Orders_PortfolioId",
          table: "Orders",
          column: "PortfolioId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "Orders");

      migrationBuilder.DropTable(
          name: "Portfolios");
    }
  }
}
