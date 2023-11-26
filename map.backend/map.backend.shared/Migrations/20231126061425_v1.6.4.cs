using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace map.backend.shared.Migrations
{
    /// <inheritdoc />
    public partial class v164 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_location_users",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    locationid = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    userid = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    record_stat = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false),
                    mod_no = table.Column<int>(type: "integer", nullable: true),
                    create_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    create_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    modify_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    modify_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tb_location_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_location_users_history",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    locationid = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    userid = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    backupdt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    record_stat = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false),
                    mod_no = table.Column<int>(type: "integer", nullable: true),
                    create_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    create_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    modify_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    modify_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tb_location_users_history", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_location_users");

            migrationBuilder.DropTable(
                name: "tb_location_users_history");
        }
    }
}
