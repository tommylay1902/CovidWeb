﻿// <auto-generated />
using System;
using CovidWebApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CovidWebApp.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CovidWebApp.Models.CaseData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Cases")
                        .HasColumnType("int");

                    b.Property<int?>("CityId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Deaths")
                        .HasColumnType("int");

                    b.Property<int>("Tested")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("CaseDatas");
                });

            modelBuilder.Entity("CovidWebApp.Models.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CityName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Population")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("CovidWebApp.Models.CaseData", b =>
                {
                    b.HasOne("CovidWebApp.Models.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId");
                });
#pragma warning restore 612, 618
        }
    }
}
