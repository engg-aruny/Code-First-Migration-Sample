using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Code_First_Migration_Sample.Migrations
{
    /// <inheritdoc />
    public partial class AddGenderColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Teachers",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "ID",
                keyValue: 1,
                column: "DateOfBirth",
                value: new DateTime(2023, 4, 4, 8, 4, 47, 994, DateTimeKind.Local).AddTicks(1655));

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "ID",
                keyValue: 2,
                column: "DateOfBirth",
                value: new DateTime(2023, 4, 4, 8, 4, 47, 994, DateTimeKind.Local).AddTicks(1664));

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "ID",
                keyValue: 1,
                column: "Gender",
                value: "M");

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "ID",
                keyValue: 2,
                column: "Gender",
                value: "M");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Teachers");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "ID",
                keyValue: 1,
                column: "DateOfBirth",
                value: new DateTime(2023, 4, 4, 7, 11, 15, 503, DateTimeKind.Local).AddTicks(5904));

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "ID",
                keyValue: 2,
                column: "DateOfBirth",
                value: new DateTime(2023, 4, 4, 7, 11, 15, 503, DateTimeKind.Local).AddTicks(5919));
        }
    }
}
