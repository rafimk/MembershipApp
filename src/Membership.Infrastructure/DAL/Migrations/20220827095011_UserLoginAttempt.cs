using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Membership.Infrastructure.DAL.Migrations
{
    public partial class UserLoginAttempt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastIncorrectLogin",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Locked",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "NumberOfIncorrectLoginAttempts",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "UserLoginAttempts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NumberOfIncorrectLoginAttempts = table.Column<int>(type: "integer", nullable: false),
                    LastIncorrectLogin = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Locked = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLoginAttempts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserLoginAttempts");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastIncorrectLogin",
                table: "Users",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Locked",
                table: "Users",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfIncorrectLoginAttempts",
                table: "Users",
                type: "integer",
                nullable: true);
        }
    }
}
