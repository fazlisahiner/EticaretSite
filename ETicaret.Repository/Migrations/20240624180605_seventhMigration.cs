using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETicaret.Repository.Migrations
{
    /// <inheritdoc />
    public partial class seventhMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 24, 21, 6, 4, 851, DateTimeKind.Local).AddTicks(2619),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 24, 20, 47, 56, 150, DateTimeKind.Local).AddTicks(3309));

            migrationBuilder.CreateTable(
                name: "Emplooyee",
                columns: table => new
                {
                    EmplooyeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TelNumber1 = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    TownId = table.Column<int>(type: "int", nullable: false),
                    DistrictId = table.Column<int>(type: "int", nullable: false),
                    NeighbourhoodId = table.Column<int>(type: "int", nullable: false),
                    AddressText = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emplooyee", x => x.EmplooyeeId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Emplooyee");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 24, 20, 47, 56, 150, DateTimeKind.Local).AddTicks(3309),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 24, 21, 6, 4, 851, DateTimeKind.Local).AddTicks(2619));
        }
    }
}
