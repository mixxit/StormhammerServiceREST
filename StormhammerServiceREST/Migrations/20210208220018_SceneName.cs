using Microsoft.EntityFrameworkCore.Migrations;

namespace StormhammerServiceREST.Migrations
{
    public partial class SceneName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SceneName",
                table: "Zone",
                maxLength: 40,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SceneName",
                table: "Zone");
        }
    }
}
