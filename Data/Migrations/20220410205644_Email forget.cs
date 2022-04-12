using Microsoft.EntityFrameworkCore.Migrations;

namespace Itransition.Data.Migrations
{
    public partial class Emailforget : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "data",
                table: "Дотеры",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                schema: "data",
                table: "Дотеры");
        }
    }
}
