using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaxRateScheduler.Migrations
{
    public partial class taxrate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblTaxRates",
                columns: table => new
                {
                    MunicipalityName = table.Column<string>(nullable: false),
                    ScheduleType = table.Column<string>(nullable: false),
                    StartDate = table.Column<DateTime>(type: "date", nullable: false),
                    TaxRate = table.Column<decimal>(type: "decimal(4,2)", nullable: false),
                    Year = table.Column<int>(nullable: false),
                    EndDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblTaxRates", x => new { x.MunicipalityName, x.ScheduleType, x.StartDate });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblTaxRates");
        }
    }
}
