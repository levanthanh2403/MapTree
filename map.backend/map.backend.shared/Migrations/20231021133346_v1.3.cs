using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace map.backend.shared.Migrations
{
    /// <inheritdoc />
    public partial class v13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "treelastlocid",
                table: "tb_locations");

            migrationBuilder.CreateTable(
                name: "tb_locations_history",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    locationid = table.Column<string>(type: "text", nullable: false),
                    projectid = table.Column<string>(type: "text", nullable: false),
                    locationname = table.Column<string>(type: "text", nullable: false),
                    locationinfo = table.Column<string>(type: "text", nullable: true),
                    address = table.Column<string>(type: "text", nullable: false),
                    location = table.Column<Geometry>(type: "geometry", nullable: false),
                    locationstatus = table.Column<string>(type: "text", nullable: false),
                    treecode = table.Column<string>(type: "text", nullable: true),
                    treename = table.Column<string>(type: "text", nullable: true),
                    treeinfor = table.Column<string>(type: "text", nullable: true),
                    treetype = table.Column<string>(type: "text", nullable: true),
                    treestatus = table.Column<string>(type: "text", nullable: true),
                    record_stat = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false),
                    mod_no = table.Column<int>(type: "integer", nullable: true),
                    create_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    create_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    modify_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    modify_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tb_locations_history", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_locations_history");

            migrationBuilder.AddColumn<string>(
                name: "treelastlocid",
                table: "tb_locations",
                type: "text",
                nullable: true);
        }
    }
}
