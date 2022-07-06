using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Membership.Infrastructure.DAL.Migrations
{
    public partial class Dispute_Request : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DisputeRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MemberId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProposedAreaId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProposedMandalamId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProposedPanchayatId = table.Column<Guid>(type: "uuid", nullable: false),
                    Reason = table.Column<string>(type: "text", nullable: true),
                    JustificationComment = table.Column<string>(type: "text", nullable: true),
                    SubmittedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    SubmittedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ActionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ActionBy = table.Column<Guid>(type: "uuid", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisputeRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DisputeRequests_Areas_ProposedAreaId",
                        column: x => x.ProposedAreaId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisputeRequests_Mandalams_ProposedMandalamId",
                        column: x => x.ProposedMandalamId,
                        principalTable: "Mandalams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisputeRequests_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisputeRequests_Panchayats_ProposedPanchayatId",
                        column: x => x.ProposedPanchayatId,
                        principalTable: "Panchayats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OcrResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FrontPageId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastPageId = table.Column<Guid>(type: "uuid", nullable: true),
                    IdNumber = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    DateofBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    ExpiryDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CardNumber = table.Column<string>(type: "text", nullable: true),
                    CardType = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OcrResults", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DisputeRequests_MemberId",
                table: "DisputeRequests",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_DisputeRequests_ProposedAreaId",
                table: "DisputeRequests",
                column: "ProposedAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_DisputeRequests_ProposedMandalamId",
                table: "DisputeRequests",
                column: "ProposedMandalamId");

            migrationBuilder.CreateIndex(
                name: "IX_DisputeRequests_ProposedPanchayatId",
                table: "DisputeRequests",
                column: "ProposedPanchayatId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DisputeRequests");

            migrationBuilder.DropTable(
                name: "OcrResults");
        }
    }
}
