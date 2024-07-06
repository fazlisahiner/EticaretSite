using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETicaret.Repository.Migrations
{
    /// <inheritdoc />
    public partial class FourthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 24, 16, 37, 55, 823, DateTimeKind.Local).AddTicks(6483),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 24, 15, 52, 13, 521, DateTimeKind.Local).AddTicks(2553));

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    CityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    CityName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PlateNo = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    PhoneCode = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.CityId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 24, 15, 52, 13, 521, DateTimeKind.Local).AddTicks(2553),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 24, 16, 37, 55, 823, DateTimeKind.Local).AddTicks(6483));
        }
    }
}
