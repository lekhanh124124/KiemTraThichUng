using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KiemTraThichUng.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class db_init_260215 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BoCauHoi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaBoCauHoi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenBoCauHoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaiLieuThamKhao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsLocked = table.Column<bool>(type: "bit", nullable: false),
                    IdNguoiTao = table.Column<int>(type: "int", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdNguoiCapNhat = table.Column<int>(type: "int", nullable: true),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoCauHoi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CauHinhDeKiemTra",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCauTruc = table.Column<int>(type: "int", nullable: false),
                    MaCauHinhDeKiemTra = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenCauHinhDeKiemTra = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThoiGianLamBaiGiay = table.Column<int>(type: "int", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    ThetaMin = table.Column<double>(type: "float", nullable: false),
                    ThetaMax = table.Column<double>(type: "float", nullable: false),
                    ThetaKhoiTao = table.Column<double>(type: "float", nullable: false),
                    PriorMean = table.Column<double>(type: "float", nullable: false),
                    PriorVariance = table.Column<double>(type: "float", nullable: false),
                    StandardErrorInitial = table.Column<double>(type: "float", nullable: false),
                    IdNguoiTao = table.Column<int>(type: "int", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdNguoiCapNhat = table.Column<int>(type: "int", nullable: true),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Stt = table.Column<int>(type: "int", nullable: false),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false),
                    IdNguoiDuyet = table.Column<int>(type: "int", nullable: true),
                    NgayDuyet = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GhiChuDuyet = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CauHinhDeKiemTra", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CauHoi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CauHoiGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaCauHoi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TieuDeVeTrai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TieuDeVePhai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MediaUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GiaiThich = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCauTruc = table.Column<int>(type: "int", nullable: false),
                    IdLoaiCauHoi = table.Column<int>(type: "int", nullable: false),
                    IdMucDoNhanThuc = table.Column<int>(type: "int", nullable: true),
                    IsCauHoiCha = table.Column<bool>(type: "bit", nullable: false),
                    IdCauHoiCha = table.Column<int>(type: "int", nullable: true),
                    IsKhongDao = table.Column<bool>(type: "bit", nullable: false),
                    DoKho = table.Column<double>(type: "float", nullable: true),
                    DoKhoKhoiTao = table.Column<double>(type: "float", nullable: true),
                    DoPhanLoai = table.Column<double>(type: "float", nullable: true),
                    DoPhanLoaiKhoiTao = table.Column<double>(type: "float", nullable: true),
                    SoLuotLam = table.Column<int>(type: "int", nullable: false),
                    SoLuotDung = table.Column<int>(type: "int", nullable: false),
                    IdNguoiTao = table.Column<int>(type: "int", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdNguoiCapNhat = table.Column<int>(type: "int", nullable: true),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Stt = table.Column<int>(type: "int", nullable: false),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false),
                    IdNguoiDuyet = table.Column<int>(type: "int", nullable: true),
                    NgayDuyet = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GhiChuDuyet = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CauHoi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CauTraLoi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCauHoi = table.Column<int>(type: "int", nullable: false),
                    MaCauTraLoi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDung = table.Column<bool>(type: "bit", nullable: false),
                    PhanTramDiem = table.Column<double>(type: "float", nullable: true),
                    IsKhongDao = table.Column<bool>(type: "bit", nullable: false),
                    IsVeTrai = table.Column<bool>(type: "bit", nullable: false),
                    ViTriGachChan = table.Column<int>(type: "int", nullable: false),
                    IsThietLapRieng = table.Column<bool>(type: "bit", nullable: false),
                    IdNguoiTao = table.Column<int>(type: "int", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdNguoiCapNhat = table.Column<int>(type: "int", nullable: true),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Stt = table.Column<int>(type: "int", nullable: false),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CauTraLoi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CauTruc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaCauTruc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenCauTruc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdBoCauHoi = table.Column<int>(type: "int", nullable: false),
                    IdParent = table.Column<int>(type: "int", nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdNguoiTao = table.Column<int>(type: "int", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdNguoiCapNhat = table.Column<int>(type: "int", nullable: true),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Stt = table.Column<int>(type: "int", nullable: false),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CauTruc", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietCauHinhDeKiemTra",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCauHinhDeKiemTra = table.Column<int>(type: "int", nullable: false),
                    IdMucDoNhanThuc = table.Column<int>(type: "int", nullable: true),
                    IdLoaiCauHoi = table.Column<int>(type: "int", nullable: true),
                    SoLuongCauHoi = table.Column<int>(type: "int", nullable: false),
                    IdNguoiTao = table.Column<int>(type: "int", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdNguoiCapNhat = table.Column<int>(type: "int", nullable: true),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Stt = table.Column<int>(type: "int", nullable: false),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietCauHinhDeKiemTra", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietKetQuaKiemTra",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdKetQuaKiemTra = table.Column<int>(type: "int", nullable: false),
                    IdCauHoi = table.Column<int>(type: "int", nullable: false),
                    IdCauHoiCha = table.Column<int>(type: "int", nullable: true),
                    IsTraLoiDung = table.Column<bool>(type: "bit", nullable: true),
                    PhanTramDiem = table.Column<double>(type: "float", nullable: false),
                    DoKhoLucThi = table.Column<double>(type: "float", nullable: true),
                    DoPhanLoaiLucThi = table.Column<double>(type: "float", nullable: true),
                    StandardErrorBefore = table.Column<double>(type: "float", nullable: true),
                    StandardErrorAfter = table.Column<double>(type: "float", nullable: true),
                    ThetaBefore = table.Column<double>(type: "float", nullable: true),
                    ThetaAfter = table.Column<double>(type: "float", nullable: true),
                    ThetaTarget = table.Column<double>(type: "float", nullable: true),
                    ThongTinCauHoi = table.Column<double>(type: "float", nullable: true),
                    ThongTinTichLuyBefore = table.Column<double>(type: "float", nullable: true),
                    ThongTinTichLuyAfter = table.Column<double>(type: "float", nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietKetQuaKiemTra", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietLuaChon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdChiTietKetQuaKiemTra = table.Column<int>(type: "int", nullable: false),
                    IdCauTraLoi = table.Column<int>(type: "int", nullable: false),
                    NoiDungCauTraLoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsTraLoiDung = table.Column<bool>(type: "bit", nullable: false),
                    PhanTramDiem = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietLuaChon", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KetQuaKiemTra",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdNguoiDung = table.Column<int>(type: "int", nullable: false),
                    IdCauHinhDeKiemTra = table.Column<int>(type: "int", nullable: false),
                    ThoiGianBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThoiGianKetThuc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DiemSo = table.Column<double>(type: "float", nullable: true),
                    Theta = table.Column<double>(type: "float", nullable: true),
                    StandardError = table.Column<double>(type: "float", nullable: true),
                    IdCauHoiHienTai = table.Column<int>(type: "int", nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KetQuaKiemTra", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoaiCauHoi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaLoaiCauHoi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenLoaiCauHoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdNguoiTao = table.Column<int>(type: "int", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdNguoiCapNhat = table.Column<int>(type: "int", nullable: true),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Stt = table.Column<int>(type: "int", nullable: false),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
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
                    MaMucDo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenMucDo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdNguoiTao = table.Column<int>(type: "int", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdNguoiCapNhat = table.Column<int>(type: "int", nullable: true),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Stt = table.Column<int>(type: "int", nullable: false),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MucDoNhanThuc", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoCauHoi");

            migrationBuilder.DropTable(
                name: "CauHinhDeKiemTra");

            migrationBuilder.DropTable(
                name: "CauHoi");

            migrationBuilder.DropTable(
                name: "CauTraLoi");

            migrationBuilder.DropTable(
                name: "CauTruc");

            migrationBuilder.DropTable(
                name: "ChiTietCauHinhDeKiemTra");

            migrationBuilder.DropTable(
                name: "ChiTietKetQuaKiemTra");

            migrationBuilder.DropTable(
                name: "ChiTietLuaChon");

            migrationBuilder.DropTable(
                name: "KetQuaKiemTra");

            migrationBuilder.DropTable(
                name: "LoaiCauHoi");

            migrationBuilder.DropTable(
                name: "MucDoNhanThuc");
        }
    }
}
