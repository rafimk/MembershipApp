using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Membership.Infrastructure.DAL.Migrations
{
    public partial class Membership_Added_AddessInformation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AddressInDistrictId",
                table: "Members",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AddressInDistrictId1",
                table: "Members",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AddressInMandalamId",
                table: "Members",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AddressInMandalamId1",
                table: "Members",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AddressInPanchayatId",
                table: "Members",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AddressInPanchayatId1",
                table: "Members",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Members_AddressInDistrictId1",
                table: "Members",
                column: "AddressInDistrictId1");

            migrationBuilder.CreateIndex(
                name: "IX_Members_AddressInMandalamId1",
                table: "Members",
                column: "AddressInMandalamId1");

            migrationBuilder.CreateIndex(
                name: "IX_Members_AddressInPanchayatId1",
                table: "Members",
                column: "AddressInPanchayatId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Districts_AddressInDistrictId1",
                table: "Members",
                column: "AddressInDistrictId1",
                principalTable: "Districts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Mandalams_AddressInMandalamId1",
                table: "Members",
                column: "AddressInMandalamId1",
                principalTable: "Mandalams",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Panchayats_AddressInPanchayatId1",
                table: "Members",
                column: "AddressInPanchayatId1",
                principalTable: "Panchayats",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Districts_AddressInDistrictId1",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_Members_Mandalams_AddressInMandalamId1",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_Members_Panchayats_AddressInPanchayatId1",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_AddressInDistrictId1",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_AddressInMandalamId1",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_AddressInPanchayatId1",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "AddressInDistrictId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "AddressInDistrictId1",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "AddressInMandalamId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "AddressInMandalamId1",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "AddressInPanchayatId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "AddressInPanchayatId1",
                table: "Members");
        }
    }
}
