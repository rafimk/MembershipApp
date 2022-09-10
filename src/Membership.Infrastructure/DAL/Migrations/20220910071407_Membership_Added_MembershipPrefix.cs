using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Membership.Infrastructure.DAL.Migrations
{
    public partial class Membership_Added_MembershipPrefix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SequenceNo",
                table: "Members",
                newName: "MembershipSequenceNo");

            migrationBuilder.AddColumn<string>(
                name: "MembershipNoPrefix",
                table: "Members",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AgentId",
                table: "MemberReadModel",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SequenceNo",
                table: "MemberReadModel",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "DistrictReadModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistrictReadModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StateReadModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Prefix = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateReadModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserReadModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    MobileNumber = table.Column<string>(type: "text", nullable: true),
                    Designation = table.Column<string>(type: "text", nullable: true),
                    Role = table.Column<string>(type: "text", nullable: true),
                    StateId = table.Column<Guid>(type: "uuid", nullable: true),
                    DistrictId = table.Column<Guid>(type: "uuid", nullable: true),
                    MandalamId = table.Column<Guid>(type: "uuid", nullable: true),
                    CascadeId = table.Column<Guid>(type: "uuid", nullable: true),
                    CascadeName = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDisputeCommittee = table.Column<bool>(type: "boolean", nullable: false),
                    VerifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserReadModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserReadModel_DistrictReadModel_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "DistrictReadModel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserReadModel_MandalamReadModel_MandalamId",
                        column: x => x.MandalamId,
                        principalTable: "MandalamReadModel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserReadModel_StateReadModel_StateId",
                        column: x => x.StateId,
                        principalTable: "StateReadModel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MemberReadModel_AgentId",
                table: "MemberReadModel",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserReadModel_DistrictId",
                table: "UserReadModel",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_UserReadModel_MandalamId",
                table: "UserReadModel",
                column: "MandalamId");

            migrationBuilder.CreateIndex(
                name: "IX_UserReadModel_StateId",
                table: "UserReadModel",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberReadModel_UserReadModel_AgentId",
                table: "MemberReadModel",
                column: "AgentId",
                principalTable: "UserReadModel",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberReadModel_UserReadModel_AgentId",
                table: "MemberReadModel");

            migrationBuilder.DropTable(
                name: "UserReadModel");

            migrationBuilder.DropTable(
                name: "DistrictReadModel");

            migrationBuilder.DropTable(
                name: "StateReadModel");

            migrationBuilder.DropIndex(
                name: "IX_MemberReadModel_AgentId",
                table: "MemberReadModel");

            migrationBuilder.DropColumn(
                name: "MembershipNoPrefix",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "AgentId",
                table: "MemberReadModel");

            migrationBuilder.DropColumn(
                name: "SequenceNo",
                table: "MemberReadModel");

            migrationBuilder.RenameColumn(
                name: "MembershipSequenceNo",
                table: "Members",
                newName: "SequenceNo");
        }
    }
}
