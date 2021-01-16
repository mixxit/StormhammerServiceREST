using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StormhammerServiceREST.Migrations
{
    public partial class Start : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(maxLength: 320, nullable: true),
                    ObjectId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mob",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    MobRaceId = table.Column<long>(nullable: false),
                    MobClassId = table.Column<long>(nullable: false),
                    AccountId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mob", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MobClass",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobClass", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MobRace",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobRace", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_ObjectId",
                table: "Account",
                column: "ObjectId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Mob");

            migrationBuilder.DropTable(
                name: "MobClass");

            migrationBuilder.DropTable(
                name: "MobRace");
        }
    }
}
