using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsoleApp1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
       migrationBuilder.CreateTable(
        name: "__EFMigrationsHistory",
        columns: table => new
        {
            MigrationId = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
            ProductVersion = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
        },
        constraints: table =>
        {
            table.PrimaryKey("PK___EFMigrationsHistory", x => x.MigrationId);
        });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseStudents");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Teachers");
        }
    }
}
