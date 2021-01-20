using Microsoft.EntityFrameworkCore.Migrations;

namespace StormhammerServiceREST.Migrations
{
    public partial class ZoneForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ZoneId1",
                table: "Mob",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mob_ZoneId",
                table: "Mob",
                column: "ZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Mob_ZoneId1",
                table: "Mob",
                column: "ZoneId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Mob_Zone_ZoneId",
                table: "Mob",
                column: "ZoneId",
                principalTable: "Zone",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Mob_Zone_ZoneId1",
                table: "Mob",
                column: "ZoneId1",
                principalTable: "Zone",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mob_Zone_ZoneId",
                table: "Mob");

            migrationBuilder.DropForeignKey(
                name: "FK_Mob_Zone_ZoneId1",
                table: "Mob");

            migrationBuilder.DropIndex(
                name: "IX_Mob_ZoneId",
                table: "Mob");

            migrationBuilder.DropIndex(
                name: "IX_Mob_ZoneId1",
                table: "Mob");

            migrationBuilder.DropColumn(
                name: "ZoneId1",
                table: "Mob");
        }
    }
}
