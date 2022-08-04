using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Membership.Infrastructure.DAL.Migrations
{
    public partial class Member_District_And_State_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Professions_ProfessionId",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_Members_Qualifications_QualificationId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_ProfessionId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_QualificationId",
                table: "Members");

            migrationBuilder.AlterColumn<Guid>(
                name: "QualificationId",
                table: "Members",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProfessionId",
                table: "Members",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PassportExpiry",
                table: "Members",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<Guid>(
                name: "DistrictId",
                table: "Members",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DistrictId1",
                table: "Members",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProfessionId1",
                table: "Members",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "QualificationId1",
                table: "Members",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StateId",
                table: "Members",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "StateId1",
                table: "Members",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MemberId",
                table: "FileAttachments",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Members_DistrictId1",
                table: "Members",
                column: "DistrictId1");

            migrationBuilder.CreateIndex(
                name: "IX_Members_ProfessionId1",
                table: "Members",
                column: "ProfessionId1");

            migrationBuilder.CreateIndex(
                name: "IX_Members_QualificationId1",
                table: "Members",
                column: "QualificationId1");

            migrationBuilder.CreateIndex(
                name: "IX_Members_StateId1",
                table: "Members",
                column: "StateId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Districts_DistrictId1",
                table: "Members",
                column: "DistrictId1",
                principalTable: "Districts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Professions_ProfessionId1",
                table: "Members",
                column: "ProfessionId1",
                principalTable: "Professions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Qualifications_QualificationId1",
                table: "Members",
                column: "QualificationId1",
                principalTable: "Qualifications",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_States_StateId1",
                table: "Members",
                column: "StateId1",
                principalTable: "States",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Districts_DistrictId1",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_Members_Professions_ProfessionId1",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_Members_Qualifications_QualificationId1",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_Members_States_StateId1",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_DistrictId1",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_ProfessionId1",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_QualificationId1",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_StateId1",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "DistrictId1",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "ProfessionId1",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "QualificationId1",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "StateId1",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "FileAttachments");

            migrationBuilder.AlterColumn<Guid>(
                name: "QualificationId",
                table: "Members",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ProfessionId",
                table: "Members",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "PassportExpiry",
                table: "Members",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Members_ProfessionId",
                table: "Members",
                column: "ProfessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_QualificationId",
                table: "Members",
                column: "QualificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Professions_ProfessionId",
                table: "Members",
                column: "ProfessionId",
                principalTable: "Professions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Qualifications_QualificationId",
                table: "Members",
                column: "QualificationId",
                principalTable: "Qualifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
