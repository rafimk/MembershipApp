using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Membership.Infrastructure.DAL.Migrations
{
    public partial class Added_mandalam_to_user_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MandalamId",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_MandalamId",
                table: "Users",
                column: "MandalamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Mandalams_MandalamId",
                table: "Users",
                column: "MandalamId",
                principalTable: "Mandalams",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Mandalams_MandalamId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_MandalamId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MandalamId",
                table: "Users");
        }
    }
}
