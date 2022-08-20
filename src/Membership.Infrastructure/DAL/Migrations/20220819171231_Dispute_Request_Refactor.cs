using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Membership.Infrastructure.DAL.Migrations
{
    public partial class Dispute_Request_Refactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DisputeRequests_Areas_ProposedAreaId",
                table: "DisputeRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_DisputeRequests_Districts_DistrictId",
                table: "DisputeRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_DisputeRequests_Mandalams_ProposedMandalamId",
                table: "DisputeRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_DisputeRequests_Panchayats_ProposedPanchayatId",
                table: "DisputeRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_DisputeRequests_States_StateId",
                table: "DisputeRequests");

            migrationBuilder.RenameColumn(
                name: "StateId",
                table: "DisputeRequests",
                newName: "ToStateId");

            migrationBuilder.RenameColumn(
                name: "ProposedPanchayatId",
                table: "DisputeRequests",
                newName: "ToPanchayatId");

            migrationBuilder.RenameColumn(
                name: "ProposedMandalamId",
                table: "DisputeRequests",
                newName: "ToMandalamId");

            migrationBuilder.RenameColumn(
                name: "ProposedAreaId",
                table: "DisputeRequests",
                newName: "ToDistrictId");

            migrationBuilder.RenameColumn(
                name: "DistrictId",
                table: "DisputeRequests",
                newName: "ToAreaId");

            migrationBuilder.RenameIndex(
                name: "IX_DisputeRequests_StateId",
                table: "DisputeRequests",
                newName: "IX_DisputeRequests_ToStateId");

            migrationBuilder.RenameIndex(
                name: "IX_DisputeRequests_ProposedPanchayatId",
                table: "DisputeRequests",
                newName: "IX_DisputeRequests_ToPanchayatId");

            migrationBuilder.RenameIndex(
                name: "IX_DisputeRequests_ProposedMandalamId",
                table: "DisputeRequests",
                newName: "IX_DisputeRequests_ToMandalamId");

            migrationBuilder.RenameIndex(
                name: "IX_DisputeRequests_ProposedAreaId",
                table: "DisputeRequests",
                newName: "IX_DisputeRequests_ToDistrictId");

            migrationBuilder.RenameIndex(
                name: "IX_DisputeRequests_DistrictId",
                table: "DisputeRequests",
                newName: "IX_DisputeRequests_ToAreaId");

            migrationBuilder.AddColumn<Guid>(
                name: "FromAreaId",
                table: "DisputeRequests",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "FromDistrictId",
                table: "DisputeRequests",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "FromMandalamId",
                table: "DisputeRequests",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "FromPanchayatId",
                table: "DisputeRequests",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "FromStateId",
                table: "DisputeRequests",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_DisputeRequests_FromAreaId",
                table: "DisputeRequests",
                column: "FromAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_DisputeRequests_FromDistrictId",
                table: "DisputeRequests",
                column: "FromDistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_DisputeRequests_FromMandalamId",
                table: "DisputeRequests",
                column: "FromMandalamId");

            migrationBuilder.CreateIndex(
                name: "IX_DisputeRequests_FromPanchayatId",
                table: "DisputeRequests",
                column: "FromPanchayatId");

            migrationBuilder.CreateIndex(
                name: "IX_DisputeRequests_FromStateId",
                table: "DisputeRequests",
                column: "FromStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_DisputeRequests_Areas_FromAreaId",
                table: "DisputeRequests",
                column: "FromAreaId",
                principalTable: "Areas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DisputeRequests_Areas_ToAreaId",
                table: "DisputeRequests",
                column: "ToAreaId",
                principalTable: "Areas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DisputeRequests_Districts_FromDistrictId",
                table: "DisputeRequests",
                column: "FromDistrictId",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DisputeRequests_Districts_ToDistrictId",
                table: "DisputeRequests",
                column: "ToDistrictId",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DisputeRequests_Mandalams_FromMandalamId",
                table: "DisputeRequests",
                column: "FromMandalamId",
                principalTable: "Mandalams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DisputeRequests_Mandalams_ToMandalamId",
                table: "DisputeRequests",
                column: "ToMandalamId",
                principalTable: "Mandalams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DisputeRequests_Panchayats_FromPanchayatId",
                table: "DisputeRequests",
                column: "FromPanchayatId",
                principalTable: "Panchayats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DisputeRequests_Panchayats_ToPanchayatId",
                table: "DisputeRequests",
                column: "ToPanchayatId",
                principalTable: "Panchayats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DisputeRequests_States_FromStateId",
                table: "DisputeRequests",
                column: "FromStateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DisputeRequests_States_ToStateId",
                table: "DisputeRequests",
                column: "ToStateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DisputeRequests_Areas_FromAreaId",
                table: "DisputeRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_DisputeRequests_Areas_ToAreaId",
                table: "DisputeRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_DisputeRequests_Districts_FromDistrictId",
                table: "DisputeRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_DisputeRequests_Districts_ToDistrictId",
                table: "DisputeRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_DisputeRequests_Mandalams_FromMandalamId",
                table: "DisputeRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_DisputeRequests_Mandalams_ToMandalamId",
                table: "DisputeRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_DisputeRequests_Panchayats_FromPanchayatId",
                table: "DisputeRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_DisputeRequests_Panchayats_ToPanchayatId",
                table: "DisputeRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_DisputeRequests_States_FromStateId",
                table: "DisputeRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_DisputeRequests_States_ToStateId",
                table: "DisputeRequests");

            migrationBuilder.DropIndex(
                name: "IX_DisputeRequests_FromAreaId",
                table: "DisputeRequests");

            migrationBuilder.DropIndex(
                name: "IX_DisputeRequests_FromDistrictId",
                table: "DisputeRequests");

            migrationBuilder.DropIndex(
                name: "IX_DisputeRequests_FromMandalamId",
                table: "DisputeRequests");

            migrationBuilder.DropIndex(
                name: "IX_DisputeRequests_FromPanchayatId",
                table: "DisputeRequests");

            migrationBuilder.DropIndex(
                name: "IX_DisputeRequests_FromStateId",
                table: "DisputeRequests");

            migrationBuilder.DropColumn(
                name: "FromAreaId",
                table: "DisputeRequests");

            migrationBuilder.DropColumn(
                name: "FromDistrictId",
                table: "DisputeRequests");

            migrationBuilder.DropColumn(
                name: "FromMandalamId",
                table: "DisputeRequests");

            migrationBuilder.DropColumn(
                name: "FromPanchayatId",
                table: "DisputeRequests");

            migrationBuilder.DropColumn(
                name: "FromStateId",
                table: "DisputeRequests");

            migrationBuilder.RenameColumn(
                name: "ToStateId",
                table: "DisputeRequests",
                newName: "StateId");

            migrationBuilder.RenameColumn(
                name: "ToPanchayatId",
                table: "DisputeRequests",
                newName: "ProposedPanchayatId");

            migrationBuilder.RenameColumn(
                name: "ToMandalamId",
                table: "DisputeRequests",
                newName: "ProposedMandalamId");

            migrationBuilder.RenameColumn(
                name: "ToDistrictId",
                table: "DisputeRequests",
                newName: "ProposedAreaId");

            migrationBuilder.RenameColumn(
                name: "ToAreaId",
                table: "DisputeRequests",
                newName: "DistrictId");

            migrationBuilder.RenameIndex(
                name: "IX_DisputeRequests_ToStateId",
                table: "DisputeRequests",
                newName: "IX_DisputeRequests_StateId");

            migrationBuilder.RenameIndex(
                name: "IX_DisputeRequests_ToPanchayatId",
                table: "DisputeRequests",
                newName: "IX_DisputeRequests_ProposedPanchayatId");

            migrationBuilder.RenameIndex(
                name: "IX_DisputeRequests_ToMandalamId",
                table: "DisputeRequests",
                newName: "IX_DisputeRequests_ProposedMandalamId");

            migrationBuilder.RenameIndex(
                name: "IX_DisputeRequests_ToDistrictId",
                table: "DisputeRequests",
                newName: "IX_DisputeRequests_ProposedAreaId");

            migrationBuilder.RenameIndex(
                name: "IX_DisputeRequests_ToAreaId",
                table: "DisputeRequests",
                newName: "IX_DisputeRequests_DistrictId");

            migrationBuilder.AddForeignKey(
                name: "FK_DisputeRequests_Areas_ProposedAreaId",
                table: "DisputeRequests",
                column: "ProposedAreaId",
                principalTable: "Areas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DisputeRequests_Districts_DistrictId",
                table: "DisputeRequests",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DisputeRequests_Mandalams_ProposedMandalamId",
                table: "DisputeRequests",
                column: "ProposedMandalamId",
                principalTable: "Mandalams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DisputeRequests_Panchayats_ProposedPanchayatId",
                table: "DisputeRequests",
                column: "ProposedPanchayatId",
                principalTable: "Panchayats",
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
    }
}
