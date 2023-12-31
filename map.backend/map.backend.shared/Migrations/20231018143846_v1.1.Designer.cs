﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using map.backend.shared.Persistence;

#nullable disable

namespace map.backend.shared.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231018143846_v1.1")]
    partial class v11
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("map.backend.shared.Entities.Auth.tb_role_mapping", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("id"));

                    b.Property<string>("create_by")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("create_by");

                    b.Property<DateTime?>("create_date")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("create_date");

                    b.Property<int?>("mod_no")
                        .HasColumnType("integer")
                        .HasColumnName("mod_no");

                    b.Property<string>("modify_by")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("modify_by");

                    b.Property<DateTime?>("modify_date")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("modify_date");

                    b.Property<string>("record_stat")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("character varying(1)")
                        .HasColumnName("record_stat");

                    b.Property<string>("rolecode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("rolecode");

                    b.Property<string>("screencode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("screencode");

                    b.Property<string>("screenorg")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("screenorg");

                    b.HasKey("id")
                        .HasName("pk_tb_role_mapping");

                    b.ToTable("tb_role_mapping", (string)null);
                });

            modelBuilder.Entity("map.backend.shared.Entities.Auth.tb_roles", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("id"));

                    b.Property<string>("create_by")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("create_by");

                    b.Property<DateTime?>("create_date")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("create_date");

                    b.Property<int?>("mod_no")
                        .HasColumnType("integer")
                        .HasColumnName("mod_no");

                    b.Property<string>("modify_by")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("modify_by");

                    b.Property<DateTime?>("modify_date")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("modify_date");

                    b.Property<string>("record_stat")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("character varying(1)")
                        .HasColumnName("record_stat");

                    b.Property<string>("rolecode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("rolecode");

                    b.Property<string>("rolename")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("rolename");

                    b.HasKey("id")
                        .HasName("pk_tb_roles");

                    b.ToTable("tb_roles", (string)null);
                });

            modelBuilder.Entity("map.backend.shared.Entities.Auth.tb_screens", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("id"));

                    b.Property<string>("create_by")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("create_by");

                    b.Property<DateTime?>("create_date")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("create_date");

                    b.Property<string>("group")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("group");

                    b.Property<string>("icon")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("icon");

                    b.Property<int>("index")
                        .HasColumnType("integer")
                        .HasColumnName("index");

                    b.Property<int?>("mod_no")
                        .HasColumnType("integer")
                        .HasColumnName("mod_no");

                    b.Property<string>("modify_by")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("modify_by");

                    b.Property<DateTime?>("modify_date")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("modify_date");

                    b.Property<string>("record_stat")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("character varying(1)")
                        .HasColumnName("record_stat");

                    b.Property<string>("screencode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("screencode");

                    b.Property<string>("screenname")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("screenname");

                    b.Property<string>("screenorg")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("screenorg");

                    b.Property<string>("screenurl")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("screenurl");

                    b.HasKey("id")
                        .HasName("pk_tb_screens");

                    b.ToTable("tb_screens", (string)null);
                });

            modelBuilder.Entity("map.backend.shared.Entities.Auth.tb_user", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("id"));

                    b.Property<string>("create_by")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("create_by");

                    b.Property<DateTime?>("create_date")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("create_date");

                    b.Property<string>("email")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("email");

                    b.Property<string>("img")
                        .HasColumnType("text")
                        .HasColumnName("img");

                    b.Property<int>("limit")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(5)
                        .HasColumnName("limit");

                    b.Property<int?>("mod_no")
                        .HasColumnType("integer")
                        .HasColumnName("mod_no");

                    b.Property<string>("modify_by")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("modify_by");

                    b.Property<DateTime?>("modify_date")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("modify_date");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("password");

                    b.Property<string>("phone")
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)")
                        .HasColumnName("phone");

                    b.Property<string>("record_stat")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("character varying(1)")
                        .HasColumnName("record_stat");

                    b.Property<string>("rolecode")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("rolecode");

                    b.Property<string>("status")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(1)
                        .HasColumnType("character varying(1)")
                        .HasDefaultValue("O")
                        .HasColumnName("status");

                    b.Property<string>("userid")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("userid");

                    b.Property<string>("username")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("username");

                    b.HasKey("id")
                        .HasName("pk_tb_user");

                    b.ToTable("tb_user", (string)null);
                });

            modelBuilder.Entity("map.backend.shared.Entities.Map.tb_locations", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("id"));

                    b.Property<string>("address")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("address");

                    b.Property<string>("create_by")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("create_by");

                    b.Property<DateTime?>("create_date")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("create_date");

                    b.Property<Geometry>("location")
                        .IsRequired()
                        .HasColumnType("geometry")
                        .HasColumnName("location");

                    b.Property<string>("locationid")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("locationid");

                    b.Property<string>("locationinfo")
                        .HasColumnType("text")
                        .HasColumnName("locationinfo");

                    b.Property<string>("locationname")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("locationname");

                    b.Property<string>("locationstatus")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("locationstatus");

                    b.Property<int?>("mod_no")
                        .HasColumnType("integer")
                        .HasColumnName("mod_no");

                    b.Property<string>("modify_by")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("modify_by");

                    b.Property<DateTime?>("modify_date")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("modify_date");

                    b.Property<string>("projectid")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("projectid");

                    b.Property<string>("record_stat")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("character varying(1)")
                        .HasColumnName("record_stat");

                    b.Property<string>("treecode")
                        .HasColumnType("text")
                        .HasColumnName("treecode");

                    b.Property<string>("treeinfor")
                        .HasColumnType("text")
                        .HasColumnName("treeinfor");

                    b.Property<string>("treelastlocid")
                        .HasColumnType("text")
                        .HasColumnName("treelastlocid");

                    b.Property<string>("treename")
                        .HasColumnType("text")
                        .HasColumnName("treename");

                    b.Property<string>("treestatus")
                        .HasColumnType("text")
                        .HasColumnName("treestatus");

                    b.Property<string>("treetype")
                        .HasColumnType("text")
                        .HasColumnName("treetype");

                    b.HasKey("id")
                        .HasName("pk_tb_locations");

                    b.ToTable("tb_locations", (string)null);
                });

            modelBuilder.Entity("map.backend.shared.Entities.Map.tb_projects", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("id"));

                    b.Property<string>("contractors")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("contractors");

                    b.Property<string>("create_by")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("create_by");

                    b.Property<DateTime?>("create_date")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("create_date");

                    b.Property<DateTime?>("enddate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("enddate");

                    b.Property<string>("investor")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("investor");

                    b.Property<int?>("mod_no")
                        .HasColumnType("integer")
                        .HasColumnName("mod_no");

                    b.Property<string>("modify_by")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("modify_by");

                    b.Property<DateTime?>("modify_date")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("modify_date");

                    b.Property<DateTime>("opendate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("opendate");

                    b.Property<string>("projectdesc")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("projectdesc");

                    b.Property<string>("projectdetail")
                        .HasColumnType("text")
                        .HasColumnName("projectdetail");

                    b.Property<string>("projectid")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("projectid");

                    b.Property<string>("projectname")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("projectname");

                    b.Property<DateTime?>("receiptdate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("receiptdate");

                    b.Property<string>("record_stat")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("character varying(1)")
                        .HasColumnName("record_stat");

                    b.Property<string>("total_value")
                        .HasColumnType("text")
                        .HasColumnName("total_value");

                    b.HasKey("id")
                        .HasName("pk_tb_projects");

                    b.ToTable("tb_projects", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
