using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StormhammerServiceREST.Migrations
{
    public partial class IdentityBinary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Identity");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "Identity",
                maxLength: 128,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Identity");

            migrationBuilder.AddColumn<byte[]>(
                name: "Password",
                table: "Identity",
                type: "varbinary(128)",
                maxLength: 128,
                nullable: true);
        }
    }
}
