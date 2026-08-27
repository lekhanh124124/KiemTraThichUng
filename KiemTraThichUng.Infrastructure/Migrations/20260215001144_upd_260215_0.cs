using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KiemTraThichUng.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class upd_260215_0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoaiCauHoi");

            migrationBuilder.DropTable(
                name: "MucDoNhanThuc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoaiCauHoi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdNguoiCapNhat = table.Column<int>(type: "int", nullable: true),
                    IdNguoiTao = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    MaLoaiCauHoi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Stt = table.Column<int>(type: "int", nullable: false),
                    TenLoaiCauHoi = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiCauHoi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MucDoNhanThuc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdNguoiCapNhat = table.Column<int>(type: "int", nullable: true),
                    IdNguoiTao = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    MaMucDo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Stt = table.Column<int>(type: "int", nullable: false),
                    TenMucDo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MucDoNhanThuc", x => x.Id);
                });
        }
    }
}
