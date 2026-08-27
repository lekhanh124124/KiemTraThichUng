using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KiemTraThichUng.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class upd_260227_0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ThetaDat",
                table: "CauHinhDeKiemTra",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThetaDat",
                table: "CauHinhDeKiemTra");
        }
    }
}
