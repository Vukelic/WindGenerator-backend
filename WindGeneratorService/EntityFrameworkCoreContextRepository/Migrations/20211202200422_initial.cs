using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityFrameworkCoreContextRepository.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WindGeneratorDevices",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GeographicalLatitude = table.Column<double>(type: "float", nullable: false),
                    GeographicalLongitude = table.Column<double>(type: "float", nullable: false),
                    GeographicalLatitudeStr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GeographicalLongitudeStr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValueDec = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValueStr = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_WindGeneratorDevices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpireTokenDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Workplace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Susspend = table.Column<bool>(type: "bit", nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    FailedAttempt = table.Column<int>(type: "int", nullable: true),
                    StartTrackingInterval = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastLoginTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AppFlag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RssId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssignRoleId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_AssignRoleId",
                        column: x => x.AssignRoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WindGeneratorDevice_Histories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValueStr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValueDec = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GeographicalLatitude = table.Column<double>(type: "float", nullable: false),
                    GeographicalLongitude = table.Column<double>(type: "float", nullable: false),
                    GeographicalLatitudeStr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GeographicalLongitudeStr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentWindGeneratorDeviceId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_WindGeneratorDevice_Histories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WindGeneratorDevice_Histories_WindGeneratorDevices_ParentWindGeneratorDeviceId",
                        column: x => x.ParentWindGeneratorDeviceId,
                        principalTable: "WindGeneratorDevices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_AssignRoleId",
                table: "Users",
                column: "AssignRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_WindGeneratorDevice_Histories_ParentWindGeneratorDeviceId",
                table: "WindGeneratorDevice_Histories",
                column: "ParentWindGeneratorDeviceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "WindGeneratorDevice_Histories");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "WindGeneratorDevices");
        }
    }
}
