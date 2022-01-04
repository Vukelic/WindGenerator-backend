using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityFrameworkCoreContextRepository.Migrations
{
    public partial class Updatewindgeneratordevice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "WindGeneratorDevices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "WindGeneratorDevices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "WindGeneratorDevice_Histories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "WindGeneratorDevice_Histories",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "WindGeneratorDevices");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "WindGeneratorDevices");

            migrationBuilder.DropColumn(
                name: "City",
                table: "WindGeneratorDevice_Histories");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "WindGeneratorDevice_Histories");
        }
    }
}
