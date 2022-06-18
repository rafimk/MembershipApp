using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Membership.Infrastructure.DAL.Migrations
{
    public partial class Initial_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileAttachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    ActualFileName = table.Column<string>(type: "text", nullable: true),
                    SavedFileName = table.Column<string>(type: "text", nullable: true),
                    FileExtension = table.Column<string>(type: "text", nullable: true),
                    FileType = table.Column<string>(type: "text", nullable: true),
                    FilePath = table.Column<string>(type: "text", nullable: true),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileAttachments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MembershipPeriods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Start = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    End = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    RegistrationUntil = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipPeriods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Professions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Qualifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qualifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Prefix = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    MobileNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    AlternativeContactNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Designation = table.Column<string>(type: "text", nullable: true),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    Role = table.Column<string>(type: "text", nullable: true),
                    CascadeId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    VerifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mandalams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DistrictId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mandalams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mandalams_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    StateId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Areas_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Panchayats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    MandalamId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Panchayats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Panchayats_Mandalams_MandalamId",
                        column: x => x.MandalamId,
                        principalTable: "Mandalams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MembershipId = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    FullName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    EmiratesIdNumber = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    EmiratesIdExpiry = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    EmiratesIdFrontPage = table.Column<Guid>(type: "uuid", nullable: true),
                    EmiratesIdLastPage = table.Column<Guid>(type: "uuid", nullable: true),
                    DateOfBirth = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    MobileNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    AlternativeContactNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PassportNumber = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    PassportExpiry = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    PassportFrontPage = table.Column<Guid>(type: "uuid", nullable: true),
                    PassportLastPage = table.Column<Guid>(type: "uuid", nullable: true),
                    ProfessionId = table.Column<Guid>(type: "uuid", nullable: false),
                    QualificationId = table.Column<Guid>(type: "uuid", nullable: false),
                    BloodGroup = table.Column<int>(type: "integer", nullable: false),
                    Photo = table.Column<Guid>(type: "uuid", nullable: true),
                    HouseName = table.Column<string>(type: "text", nullable: true),
                    AddressInIndia = table.Column<string>(type: "text", nullable: true),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    AreaId = table.Column<Guid>(type: "uuid", nullable: false),
                    MandalamId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsMemberOfAnyIndianRegisteredOrganization = table.Column<bool>(type: "boolean", nullable: false),
                    IsKMCCWelfareScheme = table.Column<bool>(type: "boolean", nullable: false),
                    CollectedAmount = table.Column<double>(type: "double precision", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    VerifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Members_Areas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Members_Mandalams_MandalamId",
                        column: x => x.MandalamId,
                        principalTable: "Mandalams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Members_Professions_ProfessionId",
                        column: x => x.ProfessionId,
                        principalTable: "Professions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Members_Qualifications_QualificationId",
                        column: x => x.QualificationId,
                        principalTable: "Qualifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Areas_StateId",
                table: "Areas",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Mandalams_DistrictId",
                table: "Mandalams",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_AreaId",
                table: "Members",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_Email",
                table: "Members",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Members_MandalamId",
                table: "Members",
                column: "MandalamId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_ProfessionId",
                table: "Members",
                column: "ProfessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_QualificationId",
                table: "Members",
                column: "QualificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Panchayats_MandalamId",
                table: "Panchayats",
                column: "MandalamId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileAttachments");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "MembershipPeriods");

            migrationBuilder.DropTable(
                name: "Panchayats");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Areas");

            migrationBuilder.DropTable(
                name: "Professions");

            migrationBuilder.DropTable(
                name: "Qualifications");

            migrationBuilder.DropTable(
                name: "Mandalams");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "Districts");
        }
    }
}
