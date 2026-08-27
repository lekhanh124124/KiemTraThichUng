using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KiemTraThichUng.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class upd_260225_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HoTen",
                table: "NguoiDung");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "NguoiDung");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HoTen",
                table: "NguoiDung",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "NguoiDung",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
