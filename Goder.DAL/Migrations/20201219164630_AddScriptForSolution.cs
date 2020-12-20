using Microsoft.EntityFrameworkCore.Migrations;

namespace Goder.DAL.Migrations
{
    public partial class AddScriptForSolution : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Script",
                table: "Solutions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Script",
                table: "Solutions");
        }
    }
}
