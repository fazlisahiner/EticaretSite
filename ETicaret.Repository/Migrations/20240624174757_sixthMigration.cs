using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETicaret.Repository.Migrations
{
    /// <inheritdoc />
    public partial class sixthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 24, 20, 47, 56, 150, DateTimeKind.Local).AddTicks(3309),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 24, 16, 54, 59, 326, DateTimeKind.Local).AddTicks(604));

            migrationBuilder.CreateTable(
                name: "District",
                columns: table => new
                {
                    DistrictId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TownId = table.Column<int>(type: "int", nullable: false),
                    DistrictName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_District", x => x.DistrictId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "District");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 24, 16, 54, 59, 326, DateTimeKind.Local).AddTicks(604),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 24, 20, 47, 56, 150, DateTimeKind.Local).AddTicks(3309));
        }
    }
}
