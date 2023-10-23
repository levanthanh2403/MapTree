using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace map.backend.shared.Migrations
{
    /// <inheritdoc />
    public partial class v11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_locations",
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
                    treelastlocid = table.Column<string>(type: "text", nullable: true),
                    record_stat = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false),
                    mod_no = table.Column<int>(type: "integer", nullable: true),
                    create_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    create_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    modify_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    modify_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tb_locations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_projects",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    projectid = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    projectname = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    projectdesc = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    projectdetail = table.Column<string>(type: "text", nullable: true),
                    investor = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    contractors = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    total_value = table.Column<string>(type: "text", nullable: true),
                    opendate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    receiptdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    enddate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    record_stat = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false),
                    mod_no = table.Column<int>(type: "integer", nullable: true),
                    create_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    create_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    modify_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    modify_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tb_projects", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_role_mapping",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rolecode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    screencode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    screenorg = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    record_stat = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false),
                    mod_no = table.Column<int>(type: "integer", nullable: true),
                    create_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    create_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    modify_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    modify_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tb_role_mapping", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_roles",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rolecode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    rolename = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    record_stat = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false),
                    mod_no = table.Column<int>(type: "integer", nullable: true),
                    create_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    create_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    modify_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    modify_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tb_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_screens",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    screencode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    screenname = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    screenorg = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    group = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    screenurl = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    icon = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    index = table.Column<int>(type: "integer", nullable: false),
                    record_stat = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false),
                    mod_no = table.Column<int>(type: "integer", nullable: true),
                    create_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    create_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    modify_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    modify_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tb_screens", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_user",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userid = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    username = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    phone = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    limit = table.Column<int>(type: "integer", nullable: false, defaultValue: 5),
                    status = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false, defaultValue: "O"),
                    img = table.Column<string>(type: "text", nullable: true),
                    rolecode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    record_stat = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false),
                    mod_no = table.Column<int>(type: "integer", nullable: true),
                    create_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    create_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    modify_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    modify_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tb_user", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_locations");

            migrationBuilder.DropTable(
                name: "tb_projects");

            migrationBuilder.DropTable(
                name: "tb_role_mapping");

            migrationBuilder.DropTable(
                name: "tb_roles");

            migrationBuilder.DropTable(
                name: "tb_screens");

            migrationBuilder.DropTable(
                name: "tb_user");
        }
    }
}
