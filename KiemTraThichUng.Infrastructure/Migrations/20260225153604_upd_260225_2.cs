using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KiemTraThichUng.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class upd_260225_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NguoiDungClaim_NguoiDung_UserId",
                table: "NguoiDungClaim");

            migrationBuilder.DropForeignKey(
                name: "FK_NguoiDungToken_NguoiDung_UserId",
                table: "NguoiDungToken");

            migrationBuilder.DropForeignKey(
                name: "FK_NguoiDungVaiTro_NguoiDung_UserId",
                table: "NguoiDungVaiTro");

            migrationBuilder.DropForeignKey(
                name: "FK_NguoiDungVaiTro_VaiTro_RoleId",
                table: "NguoiDungVaiTro");

            migrationBuilder.DropForeignKey(
                name: "FK_VaiTroClaim_VaiTro_RoleId",
                table: "VaiTroClaim");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VaiTroClaim",
                table: "VaiTroClaim");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VaiTro",
                table: "VaiTro");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NguoiDungVaiTro",
                table: "NguoiDungVaiTro");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NguoiDungToken",
                table: "NguoiDungToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NguoiDungClaim",
                table: "NguoiDungClaim");

            migrationBuilder.RenameTable(
                name: "VaiTroClaim",
                newName: "AspNetRoleClaims");

            migrationBuilder.RenameTable(
                name: "VaiTro",
                newName: "AspNetRoles");

            migrationBuilder.RenameTable(
                name: "NguoiDungVaiTro",
                newName: "AspNetUserRoles");

            migrationBuilder.RenameTable(
                name: "NguoiDungToken",
                newName: "AspNetUserTokens");

            migrationBuilder.RenameTable(
                name: "NguoiDungClaim",
                newName: "AspNetUserClaims");

            migrationBuilder.RenameIndex(
                name: "IX_VaiTroClaim_RoleId",
                table: "AspNetRoleClaims",
                newName: "IX_AspNetRoleClaims_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_NguoiDungVaiTro_RoleId",
                table: "AspNetUserRoles",
                newName: "IX_AspNetUserRoles_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_NguoiDungClaim_UserId",
                table: "AspNetUserClaims",
                newName: "IX_AspNetUserClaims_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_NguoiDung_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "NguoiDung",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_NguoiDung_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "NguoiDung",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_NguoiDung_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "NguoiDung",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_NguoiDung_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_NguoiDung_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_NguoiDung_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                newName: "NguoiDungToken");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                newName: "NguoiDungVaiTro");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                newName: "NguoiDungClaim");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                newName: "VaiTro");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                newName: "VaiTroClaim");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "NguoiDungVaiTro",
                newName: "IX_NguoiDungVaiTro_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "NguoiDungClaim",
                newName: "IX_NguoiDungClaim_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "VaiTroClaim",
                newName: "IX_VaiTroClaim_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NguoiDungToken",
                table: "NguoiDungToken",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_NguoiDungVaiTro",
                table: "NguoiDungVaiTro",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_NguoiDungClaim",
                table: "NguoiDungClaim",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VaiTro",
                table: "VaiTro",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VaiTroClaim",
                table: "VaiTroClaim",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NguoiDungClaim_NguoiDung_UserId",
                table: "NguoiDungClaim",
                column: "UserId",
                principalTable: "NguoiDung",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NguoiDungToken_NguoiDung_UserId",
                table: "NguoiDungToken",
                column: "UserId",
                principalTable: "NguoiDung",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NguoiDungVaiTro_NguoiDung_UserId",
                table: "NguoiDungVaiTro",
                column: "UserId",
                principalTable: "NguoiDung",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NguoiDungVaiTro_VaiTro_RoleId",
                table: "NguoiDungVaiTro",
                column: "RoleId",
                principalTable: "VaiTro",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VaiTroClaim_VaiTro_RoleId",
                table: "VaiTroClaim",
                column: "RoleId",
                principalTable: "VaiTro",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
