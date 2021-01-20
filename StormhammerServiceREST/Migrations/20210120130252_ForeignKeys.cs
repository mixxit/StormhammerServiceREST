using Microsoft.EntityFrameworkCore.Migrations;

namespace StormhammerServiceREST.Migrations
{
    public partial class ForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Mob_AccountId",
                table: "Mob",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Mob_MobClassId",
                table: "Mob",
                column: "MobClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Mob_MobRaceId",
                table: "Mob",
                column: "MobRaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mob_Account_AccountId",
                table: "Mob",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Mob_MobClass_MobClassId",
                table: "Mob",
                column: "MobClassId",
                principalTable: "MobClass",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Mob_MobRace_MobRaceId",
                table: "Mob",
                column: "MobRaceId",
                principalTable: "MobRace",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mob_Account_AccountId",
                table: "Mob");

            migrationBuilder.DropForeignKey(
                name: "FK_Mob_MobClass_MobClassId",
                table: "Mob");

            migrationBuilder.DropForeignKey(
                name: "FK_Mob_MobRace_MobRaceId",
                table: "Mob");

            migrationBuilder.DropIndex(
                name: "IX_Mob_AccountId",
                table: "Mob");

            migrationBuilder.DropIndex(
                name: "IX_Mob_MobClassId",
                table: "Mob");

            migrationBuilder.DropIndex(
                name: "IX_Mob_MobRaceId",
                table: "Mob");
        }
    }
}
