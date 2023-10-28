using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace map.backend.shared.Migrations
{
    /// <inheritdoc />
    public partial class v15 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "district_code",
                table: "tb_locations",
                type: "character varying(8)",
                maxLength: 8,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "province_code",
                table: "tb_locations",
                type: "character varying(8)",
                maxLength: 8,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ward_code",
                table: "tb_locations",
                type: "character varying(8)",
                maxLength: 8,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "sttm_district_standard",
                columns: table => new
                {
                    district_code = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: true),
                    province_code = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: true),
                    district_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    maker_id = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    maker_dt_stamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    checker_id = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    checker_dt_stamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    mod_no = table.Column<int>(type: "integer", nullable: true),
                    record_stat = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    district_name_value = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "sttm_province_standard",
                columns: table => new
                {
                    province_code = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: true),
                    province_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    maker_id = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    maker_dt_stamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    checker_id = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    checker_dt_stamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    mod_no = table.Column<int>(type: "integer", nullable: true),
                    record_stat = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    province_name_value = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "sttm_ward_standard",
                columns: table => new
                {
                    ward_code = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: true),
                    ward_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    district_code = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: true),
                    province_code = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: true),
                    maker_id = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    maker_dt_stamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    checker_id = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    checker_dt_stamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    mod_no = table.Column<int>(type: "integer", nullable: true),
                    record_stat = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    ward_name_value = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sttm_district_standard");

            migrationBuilder.DropTable(
                name: "sttm_province_standard");

            migrationBuilder.DropTable(
                name: "sttm_ward_standard");

            migrationBuilder.DropColumn(
                name: "district_code",
                table: "tb_locations");

            migrationBuilder.DropColumn(
                name: "province_code",
                table: "tb_locations");

            migrationBuilder.DropColumn(
                name: "ward_code",
                table: "tb_locations");
        }
    }
}
