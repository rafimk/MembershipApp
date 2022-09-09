using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Membership.Infrastructure.DAL.Migrations
{
    public partial class Membership_Added_Agent_Information : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "SequenceNo",
                startValue: 10L);

            migrationBuilder.AddColumn<Guid>(
                name: "AgentId",
                table: "Members",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SequenceNo",
                table: "Members",
                type: "bigint",
                nullable: false,
                defaultValueSql: "nextval('\"SequenceNo\"')");

            migrationBuilder.CreateTable(
                name: "AreaReadModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    StateId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreaReadModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MandalamReadModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    DistrictId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MandalamReadModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PanchayatReadModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    MandalamId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PanchayatReadModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MemberReadModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MembershipId = table.Column<string>(type: "text", nullable: true),
                    FullName = table.Column<string>(type: "text", nullable: true),
                    EmiratesIdNumber = table.Column<string>(type: "text", nullable: true),
                    EmiratesIdExpiry = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    MobileNumber = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    PassportNumber = table.Column<string>(type: "text", nullable: true),
                    StateId = table.Column<Guid>(type: "uuid", nullable: false),
                    AreaId = table.Column<Guid>(type: "uuid", nullable: false),
                    DistrictId = table.Column<Guid>(type: "uuid", nullable: false),
                    MandalamId = table.Column<Guid>(type: "uuid", nullable: false),
                    PanchayatId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CardNumber = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    MembershipPeriodId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    VerifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ManuallyEntered = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberReadModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemberReadModel_AreaReadModel_AreaId",
                        column: x => x.AreaId,
                        principalTable: "AreaReadModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemberReadModel_MandalamReadModel_MandalamId",
                        column: x => x.MandalamId,
                        principalTable: "MandalamReadModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemberReadModel_PanchayatReadModel_PanchayatId",
                        column: x => x.PanchayatId,
                        principalTable: "PanchayatReadModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Members_AgentId",
                table: "Members",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberReadModel_AreaId",
                table: "MemberReadModel",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberReadModel_MandalamId",
                table: "MemberReadModel",
                column: "MandalamId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberReadModel_PanchayatId",
                table: "MemberReadModel",
                column: "PanchayatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Users_AgentId",
                table: "Members",
                column: "AgentId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Users_AgentId",
                table: "Members");

            migrationBuilder.DropTable(
                name: "MemberReadModel");

            migrationBuilder.DropTable(
                name: "AreaReadModel");

            migrationBuilder.DropTable(
                name: "MandalamReadModel");

            migrationBuilder.DropTable(
                name: "PanchayatReadModel");

            migrationBuilder.DropIndex(
                name: "IX_Members_AgentId",
                table: "Members");

            migrationBuilder.DropSequence(
                name: "SequenceNo");

            migrationBuilder.DropColumn(
                name: "AgentId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "SequenceNo",
                table: "Members");
        }
    }
}
