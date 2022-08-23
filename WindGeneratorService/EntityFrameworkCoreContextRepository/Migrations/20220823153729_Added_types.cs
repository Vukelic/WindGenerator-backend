using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityFrameworkCoreContextRepository.Migrations
{
    public partial class Added_types : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ParentUserId",
                table: "WindGeneratorDevices",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ParentWindGeneratorTypeId",
                table: "WindGeneratorDevices",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "WindGeneratorTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Turbines = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PowerOfTurbines = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeightOfWing = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WidthOfWing = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxPowerTurbine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxSpeedTurbine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GeneratorPower = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Guarantee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoftDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsVirtual = table.Column<bool>(type: "bit", nullable: false),
                    SystemString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdditionalJsonData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoftDeleteReasonJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoftDeleteReasonInt = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WindGeneratorTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WindGeneratorDevices_ParentUserId",
                table: "WindGeneratorDevices",
                column: "ParentUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WindGeneratorDevices_ParentWindGeneratorTypeId",
                table: "WindGeneratorDevices",
                column: "ParentWindGeneratorTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_WindGeneratorDevices_Users_ParentUserId",
                table: "WindGeneratorDevices",
                column: "ParentUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WindGeneratorDevices_WindGeneratorTypes_ParentWindGeneratorTypeId",
                table: "WindGeneratorDevices",
                column: "ParentWindGeneratorTypeId",
                principalTable: "WindGeneratorTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WindGeneratorDevices_Users_ParentUserId",
                table: "WindGeneratorDevices");

            migrationBuilder.DropForeignKey(
                name: "FK_WindGeneratorDevices_WindGeneratorTypes_ParentWindGeneratorTypeId",
                table: "WindGeneratorDevices");

            migrationBuilder.DropTable(
                name: "WindGeneratorTypes");

            migrationBuilder.DropIndex(
                name: "IX_WindGeneratorDevices_ParentUserId",
                table: "WindGeneratorDevices");

            migrationBuilder.DropIndex(
                name: "IX_WindGeneratorDevices_ParentWindGeneratorTypeId",
                table: "WindGeneratorDevices");

            migrationBuilder.DropColumn(
                name: "ParentUserId",
                table: "WindGeneratorDevices");

            migrationBuilder.DropColumn(
                name: "ParentWindGeneratorTypeId",
                table: "WindGeneratorDevices");
        }
    }
}
