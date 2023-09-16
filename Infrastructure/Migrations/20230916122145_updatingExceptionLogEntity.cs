using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class updatingExceptionLogEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClassName",
                table: "ExceptionLogs",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "ExceptionLogs",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "LineNumber",
                table: "ExceptionLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClassName",
                table: "ExceptionLogs");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "ExceptionLogs");

            migrationBuilder.DropColumn(
                name: "LineNumber",
                table: "ExceptionLogs");
        }
    }
}
