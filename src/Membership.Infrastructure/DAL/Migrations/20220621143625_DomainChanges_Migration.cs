using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Membership.Infrastructure.DAL.Migrations
{
    public partial class DomainChanges_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StateId",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CardNumber",
                table: "Members",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PanchayatId",
                table: "Members",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Members_PanchayatId",
                table: "Members",
                column: "PanchayatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Panchayats_PanchayatId",
                table: "Members",
                column: "PanchayatId",
                principalTable: "Panchayats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Panchayats_PanchayatId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_PanchayatId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CardNumber",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "PanchayatId",
                table: "Members");
        }
    }
}
