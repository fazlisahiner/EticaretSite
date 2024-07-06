using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETicaret.Repository.Migrations
{
    /// <inheritdoc />
    public partial class FifthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 24, 16, 54, 59, 326, DateTimeKind.Local).AddTicks(604),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 24, 16, 37, 55, 823, DateTimeKind.Local).AddTicks(6483));

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BinaryCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    TripleCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    CountryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneCode = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.CountryId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 24, 16, 37, 55, 823, DateTimeKind.Local).AddTicks(6483),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 24, 16, 54, 59, 326, DateTimeKind.Local).AddTicks(604));
        }
    }
}
