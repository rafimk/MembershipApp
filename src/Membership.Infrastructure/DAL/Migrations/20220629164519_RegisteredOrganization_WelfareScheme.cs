using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Membership.Infrastructure.DAL.Migrations
{
    public partial class RegisteredOrganization_WelfareScheme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegistrationUntil",
                table: "MembershipPeriods");

            migrationBuilder.DropColumn(
                name: "IsKMCCWelfareScheme",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "IsMemberOfAnyIndianRegisteredOrganization",
                table: "Members");

            migrationBuilder.AddColumn<bool>(
                name: "IsDisputeCommittee",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsEnrollActive",
                table: "MembershipPeriods",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationEnded",
                table: "MembershipPeriods",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationStarted",
                table: "MembershipPeriods",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MembershipPeriodId",
                table: "Members",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "RegisteredOrganizationId",
                table: "Members",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RegisteredOrganizationId1",
                table: "Members",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WelfareSchemeId",
                table: "Members",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WelfareSchemeId1",
                table: "Members",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RegisteredOrganizations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisteredOrganizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WelfareSchemes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WelfareSchemes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Members_MembershipPeriodId",
                table: "Members",
                column: "MembershipPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_RegisteredOrganizationId1",
                table: "Members",
                column: "RegisteredOrganizationId1");

            migrationBuilder.CreateIndex(
                name: "IX_Members_WelfareSchemeId1",
                table: "Members",
                column: "WelfareSchemeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_MembershipPeriods_MembershipPeriodId",
                table: "Members",
                column: "MembershipPeriodId",
                principalTable: "MembershipPeriods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Members_RegisteredOrganizations_RegisteredOrganizationId1",
                table: "Members",
                column: "RegisteredOrganizationId1",
                principalTable: "RegisteredOrganizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_WelfareSchemes_WelfareSchemeId1",
                table: "Members",
                column: "WelfareSchemeId1",
                principalTable: "WelfareSchemes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_MembershipPeriods_MembershipPeriodId",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_Members_RegisteredOrganizations_RegisteredOrganizationId1",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_Members_WelfareSchemes_WelfareSchemeId1",
                table: "Members");

            migrationBuilder.DropTable(
                name: "RegisteredOrganizations");

            migrationBuilder.DropTable(
                name: "WelfareSchemes");

            migrationBuilder.DropIndex(
                name: "IX_Members_MembershipPeriodId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_RegisteredOrganizationId1",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_WelfareSchemeId1",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "IsDisputeCommittee",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsEnrollActive",
                table: "MembershipPeriods");

            migrationBuilder.DropColumn(
                name: "RegistrationEnded",
                table: "MembershipPeriods");

            migrationBuilder.DropColumn(
                name: "RegistrationStarted",
                table: "MembershipPeriods");

            migrationBuilder.DropColumn(
                name: "MembershipPeriodId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "RegisteredOrganizationId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "RegisteredOrganizationId1",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "WelfareSchemeId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "WelfareSchemeId1",
                table: "Members");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "RegistrationUntil",
                table: "MembershipPeriods",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<bool>(
                name: "IsKMCCWelfareScheme",
                table: "Members",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsMemberOfAnyIndianRegisteredOrganization",
                table: "Members",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
