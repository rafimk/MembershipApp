using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Membership.Infrastructure.DAL.Migrations
{
    public partial class Added_NumberOfIncorrectLoginAttempts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastIncorrectLogin",
                table: "Users",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfIncorrectLoginAttempts",
                table: "Users",
                type: "integer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastIncorrectLogin",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "NumberOfIncorrectLoginAttempts",
                table: "Users");
        }
    }
}
