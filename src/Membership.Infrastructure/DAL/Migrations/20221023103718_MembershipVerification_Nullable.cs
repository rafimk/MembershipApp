using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Membership.Infrastructure.DAL.Migrations
{
    public partial class MembershipVerification_Nullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "PassportLastPageValid",
                table: "MembershipVerifications",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<bool>(
                name: "PassportFirstPageValid",
                table: "MembershipVerifications",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.CreateIndex(
                name: "IX_MemberReadModel_DistrictId",
                table: "MemberReadModel",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberReadModel_StateId",
                table: "MemberReadModel",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberReadModel_DistrictReadModel_DistrictId",
                table: "MemberReadModel",
                column: "DistrictId",
                principalTable: "DistrictReadModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberReadModel_StateReadModel_StateId",
                table: "MemberReadModel",
                column: "StateId",
                principalTable: "StateReadModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberReadModel_DistrictReadModel_DistrictId",
                table: "MemberReadModel");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberReadModel_StateReadModel_StateId",
                table: "MemberReadModel");

            migrationBuilder.DropIndex(
                name: "IX_MemberReadModel_DistrictId",
                table: "MemberReadModel");

            migrationBuilder.DropIndex(
                name: "IX_MemberReadModel_StateId",
                table: "MemberReadModel");

            migrationBuilder.AlterColumn<bool>(
                name: "PassportLastPageValid",
                table: "MembershipVerifications",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "PassportFirstPageValid",
                table: "MembershipVerifications",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);
        }
    }
}
