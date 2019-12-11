using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Garage2.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "vehicledetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegNr = table.Column<string>(maxLength: 12, nullable: false),
                    Color = table.Column<string>(nullable: true),
                    Model = table.Column<string>(maxLength: 32, nullable: true),
                    Manufacturer = table.Column<string>(maxLength: 32, nullable: false),
                    NOWheels = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    BeginParking = table.Column<DateTime>(nullable: false),
                    EndParking = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicledetails", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "vehicledetails");
        }
    }
}
