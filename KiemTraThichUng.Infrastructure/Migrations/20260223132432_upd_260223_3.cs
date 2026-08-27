using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KiemTraThichUng.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class upd_260223_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MaCauHinhDeKiemTra",
                table: "CauHinhDeKiemTra",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                computedColumnSql: "RIGHT('0' + CAST(YEAR(NgayTao) % 100 AS VARCHAR(2)), 2) + RIGHT('0' + CAST(MONTH(NgayTao) AS VARCHAR(2)), 2) + RIGHT('0' + CAST(DAY(NgayTao) AS VARCHAR(2)), 2) + RIGHT(REPLICATE('0',9) + CAST(Id AS VARCHAR(9)),9)",
                stored: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MaCauHinhDeKiemTra",
                table: "CauHinhDeKiemTra",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15,
                oldComputedColumnSql: "RIGHT('0' + CAST(YEAR(NgayTao) % 100 AS VARCHAR(2)), 2) + RIGHT('0' + CAST(MONTH(NgayTao) AS VARCHAR(2)), 2) + RIGHT('0' + CAST(DAY(NgayTao) AS VARCHAR(2)), 2) + RIGHT(REPLICATE('0',9) + CAST(Id AS VARCHAR(9)),9)");
        }
    }
}
