using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityFrameworkCoreContextRepository.Migrations
{
    public partial class Updated_windType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BasePrice",
                table: "WindGeneratorTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InstallationCosts",
                table: "WindGeneratorTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BasePrice",
                table: "WindGeneratorTypes");

            migrationBuilder.DropColumn(
                name: "InstallationCosts",
                table: "WindGeneratorTypes");
        }
    }
}
