using Microsoft.EntityFrameworkCore.Migrations;

namespace StormhammerServiceREST.Migrations
{
    public partial class Zone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "X",
                table: "Mob",
                nullable: false,
                defaultValueSql: "0");

            migrationBuilder.AddColumn<float>(
                name: "Y",
                table: "Mob",
                nullable: false,
                defaultValueSql: "0");

            migrationBuilder.AddColumn<float>(
                name: "Z",
                table: "Mob",
                nullable: false,
                defaultValueSql: "0");

            migrationBuilder.AddColumn<long>(
                name: "ZoneId",
                table: "Mob",
                nullable: false,
                defaultValueSql: "1");

            migrationBuilder.CreateTable(
                name: "Zone",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 80, nullable: false),
                    SafeX = table.Column<float>(nullable: false, defaultValueSql: "0"),
                    SafeY = table.Column<float>(nullable: false, defaultValueSql: "0"),
                    SafeZ = table.Column<float>(nullable: false, defaultValueSql: "0")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zone", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Zone");

            migrationBuilder.DropColumn(
                name: "X",
                table: "Mob");

            migrationBuilder.DropColumn(
                name: "Y",
                table: "Mob");

            migrationBuilder.DropColumn(
                name: "Z",
                table: "Mob");

            migrationBuilder.DropColumn(
                name: "ZoneId",
                table: "Mob");
        }
    }
}
