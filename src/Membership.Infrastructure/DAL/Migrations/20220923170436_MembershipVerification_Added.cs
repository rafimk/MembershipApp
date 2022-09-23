using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Membership.Infrastructure.DAL.Migrations
{
    public partial class MembershipVerification_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "VerificationId",
                table: "Members",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MembershipVerifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MemberId = table.Column<Guid>(type: "uuid", nullable: false),
                    EidFrontPage = table.Column<Guid>(type: "uuid", nullable: false),
                    EidLastPage = table.Column<Guid>(type: "uuid", nullable: false),
                    PassportFirstPage = table.Column<Guid>(type: "uuid", nullable: true),
                    PassportLastPage = table.Column<Guid>(type: "uuid", nullable: true),
                    EdiFrontAndBackSideValid = table.Column<bool>(type: "boolean", nullable: false),
                    EidNumberValid = table.Column<bool>(type: "boolean", nullable: false),
                    EidFullNameValid = table.Column<bool>(type: "boolean", nullable: false),
                    EidNationalityValid = table.Column<bool>(type: "boolean", nullable: false),
                    EidDOBValid = table.Column<bool>(type: "boolean", nullable: false),
                    EidDOEValid = table.Column<bool>(type: "boolean", nullable: false),
                    EidIssuePlaceValid = table.Column<bool>(type: "boolean", nullable: false),
                    PassportFirstPageValid = table.Column<bool>(type: "boolean", nullable: false),
                    PassportLastPageValid = table.Column<bool>(type: "boolean", nullable: false),
                    CardType = table.Column<string>(type: "text", nullable: false),
                    MemberVerified = table.Column<bool>(type: "boolean", nullable: false),
                    Gender = table.Column<string>(type: "text", nullable: false),
                    VerifiedUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    VerifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipVerifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MembershipVerifications_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MembershipVerifications_Users_VerifiedUserId",
                        column: x => x.VerifiedUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MembershipVerifications_MemberId",
                table: "MembershipVerifications",
                column: "MemberId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MembershipVerifications_VerifiedUserId",
                table: "MembershipVerifications",
                column: "VerifiedUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MembershipVerifications");

            migrationBuilder.DropColumn(
                name: "VerificationId",
                table: "Members");
        }
    }
}
