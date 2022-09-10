using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Membership.Infrastructure.DAL.Migrations
{
    public partial class FileUpload_Added_UserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SequenceNo",
                table: "MemberReadModel",
                newName: "MembershipSequenceNo");

            migrationBuilder.AddColumn<string>(
                name: "MembershipNoPrefix",
                table: "MemberReadModel",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "FileAttachments",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DisputeRequestReadModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MemberId = table.Column<Guid>(type: "uuid", nullable: false),
                    ToStateId = table.Column<Guid>(type: "uuid", nullable: false),
                    ToAreaId = table.Column<Guid>(type: "uuid", nullable: false),
                    ToDistrictId = table.Column<Guid>(type: "uuid", nullable: false),
                    ToMandalamId = table.Column<Guid>(type: "uuid", nullable: false),
                    ToPanchayatId = table.Column<Guid>(type: "uuid", nullable: false),
                    FromStateId = table.Column<Guid>(type: "uuid", nullable: false),
                    FromAreaId = table.Column<Guid>(type: "uuid", nullable: false),
                    FromDistrictId = table.Column<Guid>(type: "uuid", nullable: false),
                    FromMandalamId = table.Column<Guid>(type: "uuid", nullable: false),
                    FromPanchayatId = table.Column<Guid>(type: "uuid", nullable: false),
                    Reason = table.Column<string>(type: "text", nullable: true),
                    JustificationComment = table.Column<string>(type: "text", nullable: true),
                    SubmittedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    SubmittedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ActionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ActionBy = table.Column<Guid>(type: "uuid", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisputeRequestReadModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DisputeRequestReadModel_AreaReadModel_FromAreaId",
                        column: x => x.FromAreaId,
                        principalTable: "AreaReadModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisputeRequestReadModel_AreaReadModel_ToAreaId",
                        column: x => x.ToAreaId,
                        principalTable: "AreaReadModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisputeRequestReadModel_DistrictReadModel_FromDistrictId",
                        column: x => x.FromDistrictId,
                        principalTable: "DistrictReadModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisputeRequestReadModel_DistrictReadModel_ToDistrictId",
                        column: x => x.ToDistrictId,
                        principalTable: "DistrictReadModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisputeRequestReadModel_MandalamReadModel_FromMandalamId",
                        column: x => x.FromMandalamId,
                        principalTable: "MandalamReadModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisputeRequestReadModel_MandalamReadModel_ToMandalamId",
                        column: x => x.ToMandalamId,
                        principalTable: "MandalamReadModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisputeRequestReadModel_MemberReadModel_MemberId",
                        column: x => x.MemberId,
                        principalTable: "MemberReadModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisputeRequestReadModel_PanchayatReadModel_FromPanchayatId",
                        column: x => x.FromPanchayatId,
                        principalTable: "PanchayatReadModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisputeRequestReadModel_PanchayatReadModel_ToPanchayatId",
                        column: x => x.ToPanchayatId,
                        principalTable: "PanchayatReadModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisputeRequestReadModel_StateReadModel_FromStateId",
                        column: x => x.FromStateId,
                        principalTable: "StateReadModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisputeRequestReadModel_StateReadModel_ToStateId",
                        column: x => x.ToStateId,
                        principalTable: "StateReadModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DisputeRequestReadModel_FromAreaId",
                table: "DisputeRequestReadModel",
                column: "FromAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_DisputeRequestReadModel_FromDistrictId",
                table: "DisputeRequestReadModel",
                column: "FromDistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_DisputeRequestReadModel_FromMandalamId",
                table: "DisputeRequestReadModel",
                column: "FromMandalamId");

            migrationBuilder.CreateIndex(
                name: "IX_DisputeRequestReadModel_FromPanchayatId",
                table: "DisputeRequestReadModel",
                column: "FromPanchayatId");

            migrationBuilder.CreateIndex(
                name: "IX_DisputeRequestReadModel_FromStateId",
                table: "DisputeRequestReadModel",
                column: "FromStateId");

            migrationBuilder.CreateIndex(
                name: "IX_DisputeRequestReadModel_MemberId",
                table: "DisputeRequestReadModel",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_DisputeRequestReadModel_ToAreaId",
                table: "DisputeRequestReadModel",
                column: "ToAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_DisputeRequestReadModel_ToDistrictId",
                table: "DisputeRequestReadModel",
                column: "ToDistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_DisputeRequestReadModel_ToMandalamId",
                table: "DisputeRequestReadModel",
                column: "ToMandalamId");

            migrationBuilder.CreateIndex(
                name: "IX_DisputeRequestReadModel_ToPanchayatId",
                table: "DisputeRequestReadModel",
                column: "ToPanchayatId");

            migrationBuilder.CreateIndex(
                name: "IX_DisputeRequestReadModel_ToStateId",
                table: "DisputeRequestReadModel",
                column: "ToStateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DisputeRequestReadModel");

            migrationBuilder.DropColumn(
                name: "MembershipNoPrefix",
                table: "MemberReadModel");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "FileAttachments");

            migrationBuilder.RenameColumn(
                name: "MembershipSequenceNo",
                table: "MemberReadModel",
                newName: "SequenceNo");
        }
    }
}
