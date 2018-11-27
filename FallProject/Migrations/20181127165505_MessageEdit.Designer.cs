﻿// <auto-generated />
using System.Collections.Generic;
using FallProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace FallProject.Migrations
{
    [DbContext(typeof(fallprojectContext))]
    [Migration("20181127165505_MessageEdit")]
    partial class MessageEdit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.0-preview3-35497")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("FallProject.Models.GuildMember", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("GuildId");

                    b.Property<string>("UnmuteAt");

                    b.HasKey("Id");

                    b.ToTable("GuildMembers");
                });

            modelBuilder.Entity("FallProject.Models.Message", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnName("id");

                    b.Property<string>("AuthorId")
                        .IsRequired()
                        .HasColumnName("authorid");

                    b.Property<string>("ChannelId")
                        .IsRequired()
                        .HasColumnName("channelid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnName("content");

                    b.Property<List<string>>("Edits");

                    b.Property<string>("GuildId")
                        .IsRequired()
                        .HasColumnName("guildid");

                    b.HasKey("Id");

                    b.ToTable("message");
                });
#pragma warning restore 612, 618
        }
    }
}
