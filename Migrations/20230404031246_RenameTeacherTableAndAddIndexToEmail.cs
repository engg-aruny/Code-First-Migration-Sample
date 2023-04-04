using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Code_First_Migration_Sample.Migrations
{
    /// <inheritdoc />
    public partial class RenameTeacherTableAndAddIndexToEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Teachers",
                table: "Teachers");

            migrationBuilder.RenameTable(
                name: "Teachers",
                newName: "Educators");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Educators",
                table: "Educators",
                column: "ID");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "ID",
                keyValue: 1,
                column: "DateOfBirth",
                value: new DateTime(2023, 4, 4, 8, 42, 46, 118, DateTimeKind.Local).AddTicks(2934));

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "ID",
                keyValue: 2,
                column: "DateOfBirth",
                value: new DateTime(2023, 4, 4, 8, 42, 46, 118, DateTimeKind.Local).AddTicks(2946));

            migrationBuilder.CreateIndex(
                name: "IX_Educators_Email",
                table: "Educators",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Educators",
                table: "Educators");

            migrationBuilder.DropIndex(
                name: "IX_Educators_Email",
                table: "Educators");

            migrationBuilder.RenameTable(
                name: "Educators",
                newName: "Teachers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Teachers",
                table: "Teachers",
                column: "ID");

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
        }
    }
}
