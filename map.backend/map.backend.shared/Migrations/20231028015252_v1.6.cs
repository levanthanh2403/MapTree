using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace map.backend.shared.Migrations
{
    /// <inheritdoc />
    public partial class v16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "address",
                table: "tb_locations",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<string>(
                name: "address_detail",
                table: "tb_locations",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "create_by",
                table: "sttm_ward_standard",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "create_date",
                table: "sttm_ward_standard",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "id",
                table: "sttm_ward_standard",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "modify_by",
                table: "sttm_ward_standard",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "modify_date",
                table: "sttm_ward_standard",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "create_by",
                table: "sttm_province_standard",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "create_date",
                table: "sttm_province_standard",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "id",
                table: "sttm_province_standard",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "modify_by",
                table: "sttm_province_standard",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "modify_date",
                table: "sttm_province_standard",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "create_by",
                table: "sttm_district_standard",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "create_date",
                table: "sttm_district_standard",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "id",
                table: "sttm_district_standard",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "modify_by",
                table: "sttm_district_standard",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "modify_date",
                table: "sttm_district_standard",
                type: "timestamp without time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "address_detail",
                table: "tb_locations");

            migrationBuilder.DropColumn(
                name: "create_by",
                table: "sttm_ward_standard");

            migrationBuilder.DropColumn(
                name: "create_date",
                table: "sttm_ward_standard");

            migrationBuilder.DropColumn(
                name: "id",
                table: "sttm_ward_standard");

            migrationBuilder.DropColumn(
                name: "modify_by",
                table: "sttm_ward_standard");

            migrationBuilder.DropColumn(
                name: "modify_date",
                table: "sttm_ward_standard");

            migrationBuilder.DropColumn(
                name: "create_by",
                table: "sttm_province_standard");

            migrationBuilder.DropColumn(
                name: "create_date",
                table: "sttm_province_standard");

            migrationBuilder.DropColumn(
                name: "id",
                table: "sttm_province_standard");

            migrationBuilder.DropColumn(
                name: "modify_by",
                table: "sttm_province_standard");

            migrationBuilder.DropColumn(
                name: "modify_date",
                table: "sttm_province_standard");

            migrationBuilder.DropColumn(
                name: "create_by",
                table: "sttm_district_standard");

            migrationBuilder.DropColumn(
                name: "create_date",
                table: "sttm_district_standard");

            migrationBuilder.DropColumn(
                name: "id",
                table: "sttm_district_standard");

            migrationBuilder.DropColumn(
                name: "modify_by",
                table: "sttm_district_standard");

            migrationBuilder.DropColumn(
                name: "modify_date",
                table: "sttm_district_standard");

            migrationBuilder.AlterColumn<string>(
                name: "address",
                table: "tb_locations",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);
        }
    }
}
