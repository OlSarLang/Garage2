using Microsoft.EntityFrameworkCore.Migrations;

namespace Garage2.Migrations
{
    public partial class IDsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_vehicledetails",
                table: "vehicledetails");

            migrationBuilder.AlterColumn<string>(
                name: "RegNr",
                table: "vehicledetails",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "vehicledetails",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_vehicledetails",
                table: "vehicledetails",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "VehicleViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegNr = table.Column<string>(nullable: true),
                    Manufacturer = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleViewModel", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehicleViewModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_vehicledetails",
                table: "vehicledetails");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "vehicledetails");

            migrationBuilder.AlterColumn<string>(
                name: "RegNr",
                table: "vehicledetails",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_vehicledetails",
                table: "vehicledetails",
                column: "RegNr");
        }
    }
}
