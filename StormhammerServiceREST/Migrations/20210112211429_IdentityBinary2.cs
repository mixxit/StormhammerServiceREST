using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StormhammerServiceREST.Migrations
{
    public partial class IdentityBinary2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Identity");

            migrationBuilder.AddColumn<byte[]>(
                name: "Password",
                table: "Identity",
                maxLength: 128,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Identity");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "Identity",
                type: "varbinary(128)",
                maxLength: 128,
                nullable: true);
        }
    }
}
