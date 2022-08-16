using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Membership.Infrastructure.DAL.Migrations
{
    public partial class Updated_Dispute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DistrictId",
                table: "DisputeRequests",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "StateId",
                table: "DisputeRequests",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_DisputeRequests_DistrictId",
                table: "DisputeRequests",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_DisputeRequests_StateId",
                table: "DisputeRequests",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_DisputeRequests_Districts_DistrictId",
                table: "DisputeRequests",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DisputeRequests_States_StateId",
                table: "DisputeRequests",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DisputeRequests_Districts_DistrictId",
                table: "DisputeRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_DisputeRequests_States_StateId",
                table: "DisputeRequests");

            migrationBuilder.DropIndex(
                name: "IX_DisputeRequests_DistrictId",
                table: "DisputeRequests");

            migrationBuilder.DropIndex(
                name: "IX_DisputeRequests_StateId",
                table: "DisputeRequests");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "DisputeRequests");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "DisputeRequests");
        }
    }
}
