using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace map.backend.shared.Migrations
{
    /// <inheritdoc />
    public partial class v165 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "histid",
                table: "tb_location_users_history",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "histid",
                table: "tb_location_users_history");
        }
    }
}
